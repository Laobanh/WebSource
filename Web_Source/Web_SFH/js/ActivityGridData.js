class ActivityGridData { 
    static Grid = {
        url: '../Handlers/Handler_GetActivities.ashx',
        width: GridWidth(),
        widthPlus:0,
        searchDate: {},
        popup: false,
        id: 'ID',
        columns: [
            {
                value: 'Date',
                field: 'Date',
                title: 'DATE',
                width: 200,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                datatype: 'Datetime',
                dataformat:'MM/dd/yyyy HH:mm:ss',
                //hidden: false,
                template: ""

            },
            {
                value: 'UserName',
                field: 'UserName',
                title: 'USER',
                width: 160,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },
            {
                value: 'Action',
                field: 'Action',
                title: 'ACTION',
                width: 225,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },
            {
                value: 'Details',
                field: 'Details',
                title: 'DETAILS',
                width: 400,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },
            {
                field: '',
                title: '',
                width: 15,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            }

        ]
    }
}