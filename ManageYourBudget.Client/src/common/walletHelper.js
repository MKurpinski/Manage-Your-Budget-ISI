const categories = [
    {text: 'Home', key: 'Home', value: '1'},
    {text: 'Business', key: 'Business', value: '0'}
];

const currencies = [
    {key: 'PLN', value: '0', flag: 'pl', text: 'PLN'},
    {key: 'USD', value: '1', flag: 'us', text: 'USD'},
    {key: 'EUR', value: '2', flag: 'eu', text: 'EUR'},
    {key: 'CHF', value: '3', flag: 'ch', text: 'CHF'},
    {key: 'GBP', value: '4', flag: 'gb', text: 'GBP'},
];

const roles = [
    {key: 'Admin', value: '0', text: 'Admin'},
    {key: 'Creator', value: '1', text: 'Creator'},
    {key: 'Normal', value: '3', text: 'Normal'},
];

const expenseTypes = [
    {key: 'Expense', value: '0', text: 'Expense'},
    {key: 'Income', value: '1', text: 'Income'},
];


const privilegesMapping = {
    Admin: true,
    Creator: true,
    Normal: false,
    InActive: false
};

const incomeCategories = [
    {key: 'Salary', value: '10000', text: 'Salary'},
    {key: 'Business', value: '10001', text: 'Business'},
    {key: 'Gift', value: '10002', text: 'Gift'},
    {key: 'Extra', value: '10003', text: 'Extra income'},
    {key: 'Loan', value: '10004', text: 'Loan'},
    {key: 'Other', value: '10', text: 'Other'},
];

const expenseCategories = [
    {key: 'Entertaiment', value: '0', text: 'Entertaiment'},
    {key: 'Bill', value: '1', text: 'Bill'},
    {key: 'Shopping', value: '2', text: 'Shopping'},
    {key: 'Food', value: '3', text: 'Food'},
    {key: 'Transport', value: '4', text: 'Transport'},
    {key: 'Home', value: '5', text: 'Home'},
    {key: 'Car', value: '6', text: 'Car'},
    {key: 'Travel', value: '7', text: 'Travel'},
    {key: 'Education', value: '8', text: 'Education'},
    {key: 'Hobby', value: '9', text: 'Hobby'},
    {key: 'Other', value: '10', text: 'Other'},
];

const currencyToFlagMapping = {
    'USD': 'us',
    'PLN': 'pl',
    'EUR': 'eu',
    'CHF': 'ch',
    'GBP': 'gb'
};

const periodTypes = [
    {key: 'Month', value: 'Month', text: 'Month'},
    {key: 'Week', value: 'Week', text: 'Week'}
];

const allType = {key: 'All', value: '100000', text: 'All'};

export default {
    categories,
    currencies,
    roles,
    expenseTypes,
    expenseCategories,
    incomeCategories,
    defaultRole: 'Normal',
    expenseType: '0',
    allCategory: '100000',
    periodTypes,
    hasAllPrivileges: role => privilegesMapping[role],
    createdByMe: role => role === 'Creator',
    adminRole: role => role === 'Admin',
    currencyToFlagCode: currency => currencyToFlagMapping[currency],
    mapStringTypeToValue: (type) => expenseTypes.find(x => x.key === type).value,
    mapValueTypeToString: (type) => expenseTypes.find(x => x.value === type).key,
    mapExpenseCategoryToValue: (type) => [...expenseCategories, ...incomeCategories].find(x => x.key === type).value,
    mapValueExpenseCategoryToString: (type) => [...expenseCategories, ...incomeCategories].find(x => x.value === type).key,
    mapStringCurrencyToValue: (currency) => currencies.find(x => x.key === currency).value,
    mapValueCurrencyToString: (currency) => currencies.find(x => x.value === currency).key,
    mapStringCategoryToValue: (category) => categories.find(x => x.key === category).value,
    mapValueCategoryToString: (category) => categories.find(x => x.value === category).key,
    mapValueRoleToString: (role) => roles.find(x => x.value === role).key,
    mapStringRoleToValue: (role) => roles.find(x => x.key === role).value,
    appendStrategy: {
        APPEND: 'APPEND',
        REPLACE: 'REPLACE'
    },
    expenseTypesSearch: [allType, ...expenseTypes],
    expenseCategoriesSearch: [allType, ...expenseCategories],
    incomeCategoriesSearch: [allType, ...incomeCategories],
    allType: [allType],
    chartTypes: {
        categoryPie: 'categoryPie',
        verticalBar: 'verticalBar'
    }
}