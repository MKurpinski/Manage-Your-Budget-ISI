import { Dropdown, Grid, Message, Responsive } from 'semantic-ui-react';
import React from 'react';
import { HorizontalBar } from 'react-chartjs-2';
import { walletHelper } from '../../common';
import moment from 'moment';

const VerticalBarChart = ({chartData, getData, onParametersChange}) => {

    const internalOnChange = async (e, {value}) => {
        await onParametersChange(walletHelper.chartTypes.verticalBar, {year: value});
        await getData(walletHelper.chartTypes.verticalBar);
    };

    chartData = {...chartData};

    const minimalYear = 2010;
    const options = Array.from(new Array(moment().year() - minimalYear),(val,index)=> index + minimalYear +1).reverse().map(val => {
        return { value: val, key: val, text: val}
    });

    return (
        <div style={{height: '100%', width: '100%'}}>
            <h3 className="centered-row"><span style={{marginRight: '5px'}}>Annual wallets's balance from</span>
                <Dropdown inline  onChange={internalOnChange} value={chartData.parameters.year} options={options}
                /> year
            </h3>
            <Grid style={{height: '100%', width: '100%'}}>
                {!chartData.data.length &&
                <Message style={{width: '100%'}}>
                    <Message.Header className="centered-row">No elements in this period of time, choose
                        another</Message.Header>
                </Message>
                }
                {!!chartData.data.length &&
                <div style={{height: '100%', width: '100%'}}>
                    <Responsive {...Responsive.onlyMobile}>
                        <HorizontalBar
                            height={400}
                            data={prepareData(chartData.data)}
                            options={{
                                maintainAspectRatio: true
                            }}
                        />
                    </Responsive>
                    <Responsive {...Responsive.onlyTablet}>
                        <HorizontalBar
                            height={200}
                            data={prepareData(chartData.data)}
                            options={{
                                maintainAspectRatio: true
                            }}
                        />
                    </Responsive>
                    <Responsive minWidth={Responsive.onlyComputer.minWidth}>
                        <HorizontalBar
                            height={150}
                            data={prepareData(chartData.data)}
                            options={{
                                maintainAspectRatio: true
                            }}
                        />
                    </Responsive>
                </div>
                }
            </Grid>
        </div>
    )
};

const colorsMapping = {
  expense: 'red',
  income: 'green'
};

const prepareData = (rawData) => {
    return {
        labels: rawData[0].labels,
        datasets: rawData.map(data => {
            return {
                label: data.type,
                data: data.points,
                backgroundColor: colorsMapping[data.type.toLowerCase()]
            }
        })
    }
};

export default VerticalBarChart;