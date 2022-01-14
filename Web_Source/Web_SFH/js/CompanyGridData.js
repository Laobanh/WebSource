class CompanyGridData {
    static d = new Date();

    static searchDate = {
        'fYear': CompanyGridData.d.getFullYear(),
        'fMonth': CompanyGridData.d.getMonth(),
        'fDay': 1,
        'tYear': CompanyGridData.d.getFullYear(),
        'tMonth': CompanyGridData.d.getMonth(),
        'tDay': 30
    };

    static Grid = {
        url: '../Handlers/Handler_GetCompanies.ashx',
        width: GridWidth(),
        widthPlus: 0,
        searchDate: CompanyGridData.searchDate,
        popup: false,
        id: 'CompanyID',
        columns: [
            {
                value: 'CompanyID',
                field: 'CompanyID',
                title: 'ID',
                width: 79,
                widthPlus: 0,
                sortable: true,
                filterable: false,
                //hidden: false,
                template: ""

            },
            {
                value: 'CompanyName',
                field: 'CompanyName',
                title: 'NAME',
                width: 265,
                widthPlus: 0,
                sortable: true,
                filterable: true,
                template: ""
            },
            {
                value: 'SumDriver',
                field: 'SumDriver',
                title: 'DONORS',
                width: 90,
                widthPlus: 0,
                sortable: true,
                filterable: true,
                template: ""
            },
            {
                value: 'Plan',
                field: 'Plan',
                title: 'PLAN',
                width: 65,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'ExpirationDate',
                field: 'ExpirationDate',
                title: 'EXPIRATION',
                width: 95,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'ConsortiumName',
                field: 'ConsortiumName',
                title: 'CONSORTIUM',
                width: 202,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },

            {
                value: '',
                field: '',
                title: 'ACTIONS',
                width: 76,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                hidden: true,
                template: "<p id='edi#CompanyID#'  class='edit' title='Edit'></p><p id='del#CompanyID#'  class='delete' title='Delete'></p>"


            },
            {
                field: '',
                title: '',
                width: 15,
                widthPlus: 0,
                sortable: false,
                filterable: false
            }

        ]
    }; 
}