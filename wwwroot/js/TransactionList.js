$(document).ready(function () {

    $('#example').DataTable({
        "ajax": {
            "url": "/Home/TransactionDetailAPI"
        },

        "columns": [
            { "data": "installments" },
            { "data": "openingAmonut" },
            { "data": "principal" },
            { "data": "interest" },
            { "data": "emi" },
            { "data": "closingAmount" },
            { "data": "cumulativeInterest" },

        ]
    });
});