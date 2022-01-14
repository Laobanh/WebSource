class WorkingAtGridData {
    static Grid = {
        url: '../Handlers/Handler_GetDonorWorkAtCompanies.ashx',
        width: 900,
        widthPlus: 0,
        searchDate: {},
        popup: false,        
        columns: [
            {
                value: 'CompanyID',
                field: 'CompanyID',
                title: 'ID',
                width: 100,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                //hidden: false,
                navigate: false,
                template: ""

            },
            {
                value: 'CompanyName',
                field: 'CompanyName',
                title: 'COMPANY NAME',
                width: 250,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false,
                template: ""
            },  
            {
                value: 'DonorWorking',
                field: 'DonorWorking',
                title: 'STATUS',
                width: 150,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false,
                bit: true,
                bitvalue:"Terminate|Active"
            },
            {
                value: 'Plan',
                field: 'Plan',
                title: 'PLAN',
                width: 100,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            },
            {
                value: 'ExpirationDate',
                field: 'ExpirationDate',
                title: 'EXPIRATION',
                width: 200,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            },            
            {
                field: '',
                title: '',
                width: 5,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            }

        ]
    }; 
}