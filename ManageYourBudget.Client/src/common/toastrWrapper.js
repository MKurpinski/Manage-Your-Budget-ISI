import * as toastr from 'toastr';

export default {
    init: () => {
        toastr.options.positionClass = 'toast-bottom-full-width';
        toastr.options.closeButton = true;
        toastr.options.preventDuplicates = true
    },
    success: (message, title) => {
        toastr.success(message, title ? title : 'Yay!')
    },
    error: (message, title) => {
        toastr.error(message, title ? title : 'Ups!')
    }
}