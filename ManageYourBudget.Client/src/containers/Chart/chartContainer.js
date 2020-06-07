import React from 'react';
import { helpers, walletHelper } from '../../common';
import { Modal, Tab } from 'semantic-ui-react';
import { chartApi } from '../../api';
import { DATE_FORMAT } from '../../common/constants';
import CategoryPieChart from '../../components/Chart/categoryPieChart';
import VerticalVarChart from '../../components/Chart/verticalBarChart';
import moment from 'moment';

export default class ChartContainer extends React.Component {
    state = {
        activeChart: walletHelper.chartTypes.categoryPie,
        activeIndex: 0,
        isLoading: true,
        charts: {
            categoryPie: {
                parameters: {
                    from: helpers.getLastMonday().format(DATE_FORMAT),
                    to: helpers.getNextSunday().format(DATE_FORMAT)
                },
                data: [],
                oldSearching: ''
            },
            verticalBar: {
                data: [],
                parameters: {year: moment().year()},
                oldSearching: ''
            }
        }
    };
    downloadData = async (chartType) => {
        const chartData = {...this.state.charts[chartType]};
        const stringifiedParameters = JSON.stringify(chartData.parameters);
        const shouldCallApi = stringifiedParameters !== chartData.oldSearching || !chartData.data;
        if (!shouldCallApi) {
            return;
        }

        this.changeLoading(true);
        const response = await chartApi.get(chartType, {...chartData.parameters, walletId: this.props.walletId});
        await this.setState(prevState => {
            return {
                charts: {
                    ...prevState.charts,
                    [chartType]: {data: response, oldSearching: stringifiedParameters, parameters: chartData.parameters}
                }
            }
        });
        this.changeLoading(false);
    };
    onParametersChange = (chartType, newParams) => {
        const chartData = {...this.state.charts[chartType]};

        this.setState(prevState => {
            return {
                charts: {
                    ...prevState.charts,
                    [chartType]: {...chartData, parameters: newParams}
                }
            }
        });
    };
    panes = [
        {
            menuItem: 'Categories', render: () =>
                <Tab.Pane style={{minHeight: '60vh'}} loading={this.state.isLoading}>
                    <CategoryPieChart getData={this.downloadData}
                                      chartData={this.state.charts[walletHelper.chartTypes.categoryPie]}
                                      onParametersChange={this.onParametersChange}
                    />
                </Tab.Pane>
        },
        {
            menuItem: 'Yearly balance', render: () =>
                <Tab.Pane style={{minHeight: '60vh'}} loading={this.state.isLoading}>
                    <VerticalVarChart getData={this.downloadData}
                                      chartData={this.state.charts[walletHelper.chartTypes.verticalBar]}
                                      onParametersChange={this.onParametersChange}
                    />
                </Tab.Pane>
        },
    ];
    panelsToChartTypesMapping = [walletHelper.chartTypes.categoryPie, walletHelper.chartTypes.verticalBar];
    setActivePanel = async (activeIndex) => {
        const activeChart = this.panelsToChartTypesMapping[activeIndex];
        this.setState({activeChart, activeIndex});
        await this.downloadData(activeChart);
    };
    changeLoading = (isLoading) => {
        this.setState({isLoading});
    };
    handleTabChange = (e, {activeIndex}) => this.setActivePanel(activeIndex);

    async componentDidMount() {
        await this.downloadData(walletHelper.chartTypes.categoryPie);
    }

    render() {
        const {isOpen, onClose} = this.props;
        return (
            <Modal style={{padding: '24px', minHeight: '70vh'}} open={isOpen} onClose={onClose}>
                <Tab panes={this.panes} activeIndex={this.state.activeIndex} onTabChange={this.handleTabChange}/>
            </Modal>
        )
    }
}