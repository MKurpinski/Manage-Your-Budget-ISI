export default {
    parseToNumberOfDecimalPoints: (decimalPlaces) => {
        return (value) => {
            let parsed = parseFloat(value);
            if(isNaN(parsed) || value.charAt(value.length - 1) === 'e'){
                return value.substring(0, value.length - 1);
            }

            return countDecimalPoints(value, parsed) > decimalPlaces ? parsed.toFixed(decimalPlaces) : parsed;
        }
    }
}

const countDecimalPoints = (value, parsed) => {
    if(Math.floor(parsed) === parsed) {
        return 0;
    }
    return value.split(".")[1].length || 0;
};