function ConsortiumGridData() {
    this.Grid = {
        url: '../Handlers/Handler_GetConsortiums.ashx',
        width: GridWidth(),
        widthPlus: 0,
        searchDate: {},
        popup: false,
        id: 'ID',
        columns: [  
            {
                value: 'ID',
                field: 'ID',
                title: 'ID',
                width: 80,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },  
            {
                value: 'Name',
                field: 'Name',
                title: 'Name',
                width: 500,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },                    
            {
                field: '',
                title: '',
                width: 5,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            }

        ]
    }
}