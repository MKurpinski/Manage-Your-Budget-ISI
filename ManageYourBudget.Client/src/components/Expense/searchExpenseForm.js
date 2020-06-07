import walletHelper from '../../common/walletHelper';
import { Dropdown, Grid, Input } from 'semantic-ui-react';
import React from 'react';
import DatePicker from 'react-datepicker';
import { SimpleButton } from '../common/buttons';
import ButtonAsInput from '../common/buttonAsInput';

const SearchExpenseForm = ({
                           searchParams,
                           onSimpleChange,
                           handleChangeStart,
                           handleChangeEnd,
                           handleTypeChange,
                           handleCategoryChange,
                           onReplaceSearch,
                           getCategoryOptions,
                           cannotSearch,
                           isLoading
                       }) => {
    const onSubmit = (e) => {
        e.preventDefault();
        if(cannotSearch() || isLoading){
            return;
        }
        onReplaceSearch();
    };

    return (
        <form onSubmit={onSubmit}>
            <Grid>
                <Grid.Column mobile={16} tablet={16} computer={4}>
                    <label>Search</label>
                    <Input
                        fluid
                        value={searchParams.searchTerm}
                        name="searchTerm"
                        onChange={onSimpleChange}
                        placeholder='Search...'/>
                </Grid.Column>
                <Grid.Column mobile={8} tablet={4} computer={2}>
                    <label>From</label>
                    <DatePicker
                        selected={searchParams.dateFrom}
                        selectsStart
                        customInput={<ButtonAsInput/>}
                        startDate={searchParams.dateFrom}
                        endDate={searchParams.dateTo}
                        name="dateFrom"
                        onChange={handleChangeStart}
                    />
                </Grid.Column>
                <Grid.Column mobile={8} tablet={4} computer={2}>
                    <label>To</label>
                    <DatePicker
                        selected={searchParams.dateTo}
                        selectsEnd
                        customInput={<ButtonAsInput/>}
                        startDate={searchParams.dateFrom}
                        endDate={searchParams.dateTo}
                        name="dateFrom"
                        onChange={handleChangeEnd}
                    />
                </Grid.Column>
                <Grid.Column mobile={8} tablet={4} computer={3}>
                    <label>Type</label>
                    <Dropdown
                        fluid
                        selection
                        value={searchParams.type}
                        options={walletHelper.expenseTypesSearch}
                        onChange={handleTypeChange}
                    />
                </Grid.Column>
                <Grid.Column mobile={8} tablet={4} computer={3}>
                    <label>Category</label>
                    <Dropdown
                        fluid
                        selection
                        value={searchParams.category}
                        options={getCategoryOptions()}
                        onChange={handleCategoryChange}
                    />
                </Grid.Column>
                <Grid.Column mobile={16} tablet={16} computer={2}>
                    <label style={{visibility: 'hidden'}}>Search</label>
                    <SimpleButton
                        onClick={onReplaceSearch}
                        disabled={cannotSearch() || isLoading}
                        className="fluid">Search
                    </SimpleButton>
                </Grid.Column>
            </Grid>
        </form>
    )
};

export default SearchExpenseForm;