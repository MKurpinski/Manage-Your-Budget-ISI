import React from 'react';
import { helpers } from '../../common/index'
import walletHelper from '../../common/walletHelper';
import { searchApi } from '../../api';
import { withRouter } from 'react-router-dom';
import queryString from 'query-string'
import SearchExpense from './searchExpenseForm';
import { DATE_FORMAT } from '../../common/constants';
import moment from 'moment';
import ExpensesSection from './expensesSection';

class ExpenseSearchWrapper extends React.PureComponent {
    BATCHSIZE = 20;

    static defaultProps = {
        searchParams: {
            searchTerm: '',
            dateFrom: helpers.getLastMonday(),
            dateTo: helpers.getNextSunday(),
            category: walletHelper.allCategory,
            type: walletHelper.allCategory
        }
    };

    constructor(props) {
        super(props);

        const mergedParams = this.mergeParams();
        this.state = {
            searchParams: mergedParams,
            loading: true,
            showMore: false,
            stringifiedParams: JSON.stringify(mergedParams),
            page: 0
        }
    }

    async componentDidMount() {
        await this.onFirstSearch();
    };

    mergeParams = () => {
        const values = queryString.parse(this.props.location.search);
        if(values.dateTo){
            values.dateTo = moment(values.dateTo, DATE_FORMAT);
        }
        if(values.dateFrom){
            values.dateFrom = moment(values.dateFrom, DATE_FORMAT);
        }
        const defaultValues = {
            searchTerm: '',
            dateFrom: helpers.getLastMonday(),
            dateTo: helpers.getNextSunday(),
            category: walletHelper.allCategory,
            type: walletHelper.allCategory
        };

        return {...defaultValues, ...values};
    };

    onSearch = async (appendStategy, isInit = false) => {
        let {searchParams, page} = this.state;
        searchParams = {...searchParams};
        searchParams.dateFrom = searchParams.dateFrom.format(DATE_FORMAT);
        searchParams.dateTo = searchParams.dateTo.format(DATE_FORMAT);

        await this.setState({loading: true});
        const searchResults = await searchApi.searchExpenses({
            ...searchParams,
            batchSize: this.BATCHSIZE,
            page: page,
            walletId: this.props.walletId
        });

        this.setState({
            loading: false,
            isMore: (this.props.expenses.length + this.BATCHSIZE) < searchResults.total,
            stringifiedParams: JSON.stringify(this.state.searchParams)
        });

        this.props.onSearch({searchResults: searchResults.results, searchParams, strategy: appendStategy, isInit});
    };

    onReplaceSearch = async () => {
        await this.setState({page: 0});
        await this.onSearch(walletHelper.appendStrategy.REPLACE);
    };

    onFirstSearch = async () => await this.onSearch(walletHelper.appendStrategy.REPLACE, true);

    onAppendSearch = async () => {
        await this.setState(prevState => {
            return {page: prevState.page + 1};
        });
        await this.onSearch(walletHelper.appendStrategy.APPEND);
    };

    onSimpleChange = (e) => {
        const target = e.target;
        this.setState(prevState => {
            return {searchParams: {...prevState.searchParams, [target.name]: target.value}}
        })
    };

    handleDataChange = ({dateFrom, dateTo}) => {
        dateFrom = dateFrom || this.state.searchParams.dateFrom;
        dateTo = dateTo || this.state.searchParams.dateTo;

        if (dateFrom.isAfter(dateTo)) {
            dateTo = dateFrom
        }

        this.setState(prevState => {
            return {searchParams: {...prevState.searchParams, dateFrom, dateTo}}
        });
    };

    handleChangeStartDate = (dateFrom) => this.handleDataChange({dateFrom});
    handleChangeEndDate = (dateTo) => this.handleDataChange({dateTo});

    getCategoryOptions = () => {
        switch (this.state.searchParams.type) {
            case '0':
                return walletHelper.expenseCategoriesSearch;
            case '1':
                return walletHelper.incomeCategoriesSearch;
            default:
                return walletHelper.allType
        }
    };

    handleTypesChange = ({type, category}) => {
        type = type || this.state.searchParams.type;

        category = category || this.state.searchParams.category;

        if (type !== this.state.searchParams.type) {
            category = walletHelper.allType[0].value;
        }

        this.setState(prevState => {
            return {
                searchParams: {
                    ...prevState.searchParams,
                    category,
                    type
                }
            }
        })
    };

    handleTypeChange = (e, {value}) => this.handleTypesChange({type: value});
    handleCategoryChange = (e, {value}) => this.handleTypesChange({category: value});

    cannotSearch = () => {
        return this.state.stringifiedParams === JSON.stringify(this.state.searchParams);
    };

    render() {
        const {searchParams} = this.state;
        return (
            <div>
                <SearchExpense
                    searchParams={searchParams}
                    cannotSearch={this.cannotSearch}
                    handleTypeChange={this.handleTypeChange}
                    handleCategoryChange={this.handleCategoryChange}
                    getCategoryOptions={this.getCategoryOptions}
                    handleChangeEnd={this.handleChangeEndDate}
                    handleChangeStart={this.handleChangeStartDate}
                    onReplaceSearch={this.onReplaceSearch}
                    onSimpleChange={this.onSimpleChange}
                    isLoading={this.state.loading}
                />
                <ExpensesSection
                    downloadCurrency={this.props.downloadCurrency}
                    onExpenseEdited={this.props.onExpenseEdited}
                    onExpenseDelete={this.props.onExpenseDelete}
                    currency={this.props.currency}
                    expenses={this.props.expenses}
                    isLoading={this.state.loading}
                    onMoreClick={this.onAppendSearch}
                    isMore={this.state.isMore}
                />
            </div>
        )
    }
}

export default withRouter(ExpenseSearchWrapper);
