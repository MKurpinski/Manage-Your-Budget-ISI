import { Grid, Message } from 'semantic-ui-react';
import React from 'react';
import DatePicker from 'react-datepicker';
import ButtonAsInput from '../common/buttonAsInput';
import walletHelper from '../../common/walletHelper';
import moment from 'moment';
import { DATE_FORMAT } from '../../common/constants';
import { Doughnut } from 'react-chartjs-2'
import { helpers } from '../../common';
import SimpleButton from '../common/buttons/simpleButton';

const CategoryPieChart = ({chartData, onParametersChange, getData}) => {
    chartData = {...chartData};

    const handleDataChange = ({dateFrom, dateTo}) => {
        dateFrom = dateFrom || chartData.parameters.from;
        dateTo = dateTo || chartData.parameters.to;

        if (dateFrom.isAfter(dateTo)) {
            dateTo = dateFrom
        }

        onParametersChange(walletHelper.chartTypes.categoryPie, {from: dateFrom.format(DATE_FORMAT), to: dateTo.format(DATE_FORMAT)});
    };

    chartData.parameters = { from: moment(chartData.parameters.from), to: moment(chartData.parameters.to)};

    const handleChangeStartDate = (dateFrom) => handleDataChange({dateFrom});
    const handleChangeEndDate = (dateTo) => handleDataChange({dateTo});

    const getDataInternal = async () => {
      await getData(walletHelper.chartTypes.categoryPie)
    };

    return (
        <div>
            <h3 className="centered-row">Operations divided into categories</h3>
            <Grid>
                <Grid.Column mobile={8} tablet={6} computer={7}>
                    <label>From</label>
                    <DatePicker
                        selected={chartData.parameters.from}
                        selectsStart
                        customInput={<ButtonAsInput/>}
                        startDate={chartData.parameters.from}
                        endDate={chartData.parameters.to}
                        name="from"
                        onChange={handleChangeStartDate}
                    />
                </Grid.Column>
                <Grid.Column mobile={8} tablet={6} computer={7}>
                    <label>To</label>
                    <DatePicker
                        selected={chartData.parameters.to}
                        selectsEnd
                        customInput={<ButtonAsInput/>}
                        startDate={chartData.parameters.from}
                        endDate={chartData.parameters.to}
                        name="to"
                        onChange={handleChangeEndDate}
                    />
                </Grid.Column>
                <Grid.Column mobile={16} tablet={4} computer={2}>
                    <label style={{visibility: 'hidden'}}>Check!</label>
                    <SimpleButton className="fluid primary" onClick={getDataInternal}>Go</SimpleButton>
                </Grid.Column>
            </Grid>
            <Grid>
                {!chartData.data.length &&
                    <Message style={{width: '100%'}}>
                        <Message.Header className="centered-row">No elements in this period of time, choose another</Message.Header>
                    </Message>
                }
                {chartData.data.map(data =>
                    <Grid.Column key={data.type} mobile={16} tablet={16/chartData.data.length} computer={16/chartData.data.length}>
                        <div className="centered-column">
                            <h3>{data.type}</h3>
                            <Doughnut data={prepareData(data)}/>
                        </div>
                    </Grid.Column>
                )}
            </Grid>
        </div>
    )
};

const prepareData = (rawData) => {
    return {
        labels: rawData.labels,
        datasets: [{
            data: rawData.points,
            backgroundColor: rawData.points.map(_ => helpers.getRandomColor())
        }]
    }
};

export default CategoryPieChart;