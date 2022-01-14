function ScheduleGridData() {
    this.Grid = {
        url: '../Handlers/Handler_GetSchedules.ashx',
        width: 1120,
        widthPlus: 15,
        searchDate: {},
        popup: false,
        id: 'ID',
        columns: [
            {
                value: 'ID',
                field: 'ID',
                title: 'ID',
                width: 85,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },
            {
                value: 'Name',
                field: 'Name',
                title: 'Description',
                width: 505,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },
            {
                value: 'Started',
                field: 'Started',
                title: 'Started',
                width: 255,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false,
                datatype: 'Datetime',
                dataformat: 'MM/dd/yyyy HH:mm:ss',
                template: ""
            },
            {
                value: 'Details.RepeatText',
                field: 'Details.RepeatText',
                title: 'Repeats',
                width: 255,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                navigate: false,
                template: ""
            },
            //{
            //    value: '',
            //    field: '',
            //    title: 'Actions',
            //    width: 270,
            //    widthPlus: 0,
            //    sortable: false,
            //    filterable: false,
            //    hidden: true,
            //    template: "<p id='del#ID#' class='delete' ></p>"
            //},
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