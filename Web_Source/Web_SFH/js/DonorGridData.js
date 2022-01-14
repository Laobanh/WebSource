class DonorGridData {
    static searchData = {
        'id': 0
    };

    static Grid = {
        url: "../Handlers/Handler_GetDonor.ashx",
        width: GridWidth(),
        widthPlus: 0,
        searchData: DonorGridData.searchData,
        popup: false,
        id: 'ID',
        columns: [            
            {
                value: 'ExcludeFromSelection',
                field: 'ExcludeFromSelection',
                title: 'Eliminate',
                width: 60,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                bit: true
                //bitValue: ""               
            },
            /*
            {
                value: 'NotActive',
                field: 'NoActive',
                title: 'NO ACTIVE',
                width: 78,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                bit: true,
                bitValue: "X"

            },
            {
                value: 'NotAvilable',
                field: 'NotAvilable',
                title: 'NO AVILLABLE',
                width: 120,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                bit: true,
                bitValue: "X"

            },
            */
            {
                value: 'Lastname',
                field: 'LastName',
                title: 'LAST',
                width: 91,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'Firstname',
                field: 'Firstname',
                title: 'FIRST',
                width: 104,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },

            {
                value: 'PeopleID',
                field: 'PeopleID',
                title: 'DONOR ID',
                width: 143,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'Mode',
                field: 'mode',
                title: 'MODE',
                width: 91,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'Category',
                field: 'Category',
                title: 'CATEGORY',
                width: 110,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'CompanyName',
                field: 'CompanyName',
                title: 'COMPANY',
                width: 280,
                widthPlus: 0,
                sortable: true,
                filterable: true,
                //hidden: true,
                template: ""


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