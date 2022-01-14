class SpecimentGridData {
    static Grid = {
        url: '../Handlers/Handler_GetTestResult.ashx',
        width: GridWidth(),
        widthPlus:0,
        searchData: {},
        popup: true,
        columns: [  
            {
                value: '',
                field: '',
                title: '',
                width: 60,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                hidden: true,
                navigate: true,
                template: "<p id='pdf#PatientID#' navi='true' class='pdf'></p>"
            },
            {
                value: 'PatientID',
                field: 'PatientID',
                title: 'TEST ID',
                width: 125,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false,
                template: ""
            },
            {
                value: 'CollectionDate',
                field: 'CollectionDate',
                title: 'COLLECTION DATE',
                width: 161,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            },
            {
                value: 'ResultDate',
                field: 'ResultDate',
                title: 'RESULT DATE',
                width: 118,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            },            
            {
                value: 'LastName',
                field: 'LastName',
                title: 'LAST',
                width: 126,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            },
            {
                value: 'FirstName',
                field: 'FirstName',
                title: 'FIRST',
                width: 166,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false
            },          
            {
                value: 'Result', //cell value
                field: 'Result',       //Cell ID
                title: 'TEST RESULT', //Head cell title
                width: 139,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false,
                colorChange: true //Change cell value color
            },
            {
                value: 'TestReason',
                field: 'TestReason',
                title: 'TEST REASON',
                width: 167,
                widthPlus: 0,
                sortable: true,
                filterable: true
            },
            {
                value: 'TestType',
                field: 'TestType',
                title: 'MODE',
                width: 97,
                widthPlus: 0,
                sortable: true,
                filterable: true
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