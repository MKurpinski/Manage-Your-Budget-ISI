import moment from 'moment';
import walletHelper from './walletHelper';

export default {
    parseToNumberOfDecimalPoints: (decimalPlaces) => {
        return (value) => {
            let parsed = parseFloat(value);
            if(isNaN(parsed) || value.charAt(value.length - 1) === 'e'){
                return value.substring(0, value.length - 1);
            }

            return countDecimalPoints(value, parsed) > decimalPlaces ? parsed.toFixed(decimalPlaces) : parsed;
        }
    },
    flatten: object => {
        return Object.assign({}, ...function flattenInternal(objToFlatten) {
            return [].concat(
                ...Object.keys(objToFlatten).map(
                    key => typeof objToFlatten[key] === 'object' ? flattenInternal(objToFlatten[key], key) : ({[key]: objToFlatten[key]})
                )
            )
        }(object));
    },
    getLastMonday: () => {
        return moment().startOf('isoWeek');
    },
    getNextSunday: () => {
        return moment().endOf('isoWeek');
    },
    prepareExpenseToSave: (expense, walletId) => {
        return {
            ...expense,
            price: (expense.price * expense.rate).toFixed(2),
            walletId: walletId
        };
    },
    prepareExpenseToDisplay: (expense) => {
        return {
            ...expense,
            type: walletHelper.mapValueTypeToString(expense.type),
            category: walletHelper.mapValueExpenseCategoryToString(expense.category)
        };
    }
}

const countDecimalPoints = (value, parsed) => {
    if(Math.floor(parsed) === parsed) {
        return 0;
    }
    return value.split(".")[1].length || 0;
};