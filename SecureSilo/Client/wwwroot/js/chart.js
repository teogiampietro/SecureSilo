google.charts.load('current', { packages: ['corechart', 'line'] });
google.charts.setOnLoadCallback(initChart);

function initChart() {
    var xs = [];
    var ys = [];
    var tiempo = "";
    var valor = "";
    window.generateChart({ xs, ys,tiempo,valor });
}

window.generateChart = (params) => {
    var xs = params.xs;
    var ys = params.ys;
    var tiempo = params.tiempo;
    var valor = params.valor;

    var data = new google.visualization.DataTable();
    data.addColumn('number', 'Dias');
    data.addColumn('number', valor);


    for (var i = 0; i < ys.length; i++) {
        data.addRow([xs[i], ys[i]]);
    };

    var options = {
        hAxis: { title: tiempo },
        vAxis: { title: valor },
        title: ' ' + valor +' de los últimos '+ ys.length +' días',
        legend: { position: 'none' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('chartDiv'));
    chart.draw(data, options);


};

window.onresize = function () { initChart(); };