$.fn.select2.defaults.set("theme", "bootstrap4");

var chartColors = {
    red: 'rgb(255, 99, 132)',
    orange: 'rgb(255, 159, 64)',
    green: 'rgb(75, 192, 192)',
    blue: 'rgb(54, 162, 235)',
    yellow: 'rgb(255, 205, 86)',
    purple: 'rgb(153, 102, 255)',
    grey: 'rgb(201, 203, 207)'
};

var chartColorsArray = [
    'rgb(255, 99, 132)',
    'rgb(255, 159, 64)',
    'rgb(75, 192, 192)',
    'rgb(54, 162, 235)',
    'rgb(255, 205, 86)',
    'rgb(153, 102, 255)',
    'rgb(201, 203, 207)'
];

var phonesArray = $('#canvas').data('phonesdata');

console.log(phonesArray);

var datasets = [];

for (i = 0; i < phonesArray.ordersdata.length; i++) {
    var phoneData = {
        label: phonesArray.ordersdata[i].label,
        fill: false,
        backgroundColor: chartColorsArray[i],
        borderColor: chartColorsArray[i],
        data: phonesArray.ordersdata[i].data
    }
    datasets.push(phoneData);
}

var config = {
    type: 'line',
    data: {
        labels: phonesArray.datesarray,
        datasets: datasets
    },
    options: {
        responsive: true,
        title: {
            display: true,
            text: 'Chart.js Line Chart'
        },
        tooltips: {
            callbacks: {
                labelColor: function (tooltipItem, chart) {
                    return {
                        borderColor: chartColorsArray[tooltipItem.datasetIndex],
                        backgroundColor: chartColorsArray[tooltipItem.datasetIndex]
                    };
                },
                labelTextColor: function (tooltipItem, chart) {
                    return chartColorsArray[tooltipItem.datasetIndex];
                },
                title: function (tooltipItem, chart) {
                    
                    var item = tooltipItem[0]
                    return item.xLabel + ': ' + item.yLabel;
                }
            }
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            xAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Month'
                }
            }],
            yAxes: [{
                display: true,
                scaleLabel: {
                    display: true,
                    labelString: 'Orders'
                }
            }]
        }
    }
};

window.onload = function () {
    var ctx = document.getElementById('canvas').getContext('2d');
    window.myLine = new Chart(ctx, config);
};