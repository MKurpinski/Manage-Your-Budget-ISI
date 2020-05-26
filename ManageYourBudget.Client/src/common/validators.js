const compare = (name, compareName, comparedKey) => (value, allValues) => {

    return value && allValues[comparedKey] && value !== allValues[comparedKey] ? `${name} must match ${compareName}` : required(name)(value);
};

const email = value => value && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(value) ? 'Invalid email address' : undefined;

const minLength = min => value => value && value.length < min ? `Must be ${min} characters or more` : undefined;

const maxLength = max => value => value && value.length > max ? `Must be ${max} characters or less` : undefined;

const required = name => value => value ? undefined : `${name} is required!`;
const biggerThan = (name, greaterThan) => value => value && parseFloat(value) <= greaterThan ? `${name} must be bigger then ${greaterThan}!` : undefined;

export default {
    required,
    maxLength,
    minLength,
    email,
    compare,
    biggerThan
}