GetReportHoiDongByTrangThai();
GetReportHoSoByTrangThai();

function GetReportHoiDongByTrangThai() {
    $.get('/Home/ReportHoiDongByTrangThai', function (res) {
        LoadReportHoiDongByTrangThai(res);
    });
};

function GetReportHoSoByTrangThai() {
    $.get('/Home/ReportHoSoByTrangThai', function (res) {
        LoadReportHoSoByTrangThai(res);
    });
};

function LoadReportHoiDongByTrangThai(lData) {
    $('canvas#ChartHoiDongByTrangThai').remove();
    $('#divChartHoiDongByTrangThai').html('<canvas id="ChartHoiDongByTrangThai" style="height:300px;"> </canvas>');

    var lstLable = [];
    var lstDataSource = [];

    $.each(lData, function (index, item) {
        lstLable.push(item.label);
        lstDataSource.push(item.countHoiDongByTrangThai)
    });
    var ctx = document.getElementById("ChartHoiDongByTrangThai");
    new Chart(ctx, {
        type: "bar",
        data: {
            labels: lstLable,
            datasets: [{
                label: "Số lượng",
                backgroundColor: "#4e73df",
                hoverBackgroundColor: "#2e59d9",
                borderColor: "#4e73df",
                data: lstDataSource

            }]
        },
        options: {
            maintainAspectRatio: false,
            title: {
                display: true,
                text: "Biểu đồ thống kê số lượng hội đồng theo trạng thái"
            },
            layout: {
                padding: {
                    left: 10,
                    right: 25,
                    top: 25,
                    bottom: 0
                }
            },
            scales: {
                xAxes: [{
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false,
                        drawBorder: false
                    },

                }],
            },
        }
    }
    )

}

function LoadReportHoSoByTrangThai(lData) {
    $('canvas#ChartHoSoByTrangThai').remove();
    $('#divChartHoSoByTrangThai').html('<canvas id="ChartHoSoByTrangThai" style="height:300px;"> </canvas>');

    var lstLable = [];
    var lstDataSource = [];

    $.each(lData, function (index, item) {
        lstLable.push(item.label);
        lstDataSource.push(item.countHoSoByTrangThai)
    });
    var ctx = document.getElementById("ChartHoSoByTrangThai");
    new Chart(ctx, {
        type: "bar",
        data: {
            labels: lstLable,
            datasets: [{
                label: "Số lượng",
                backgroundColor: "#4e73df",
                hoverBackgroundColor: "#2e59d9",
                borderColor: "#4e73df",
                data: lstDataSource

            }]
        },
        options: {
            maintainAspectRatio: false,
            title: {
                display: true,
                text: "Biểu đồ thống kê số lượng hồ sơ theo trạng thái"
            },
            layout: {
                padding: {
                    left: 10,
                    right: 25,
                    top: 25,
                    bottom: 0
                }
            },
            scales: {
                xAxes: [{
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false,
                        drawBorder: false
                    },

                }],
            },
        }
    }
    )

}