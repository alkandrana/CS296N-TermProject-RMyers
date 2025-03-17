/*
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
var showControllersOnly = false;
var seriesFilter = "";
var filtersOnlySampleSeries = true;

/*
 * Add header in statistics table to group metrics by category
 * format
 *
 */
function summaryTableHeader(header) {
    var newRow = header.insertRow(-1);
    newRow.className = "tablesorter-no-sort";
    var cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Requests";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 3;
    cell.innerHTML = "Executions";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 7;
    cell.innerHTML = "Response Times (ms)";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Throughput";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 2;
    cell.innerHTML = "Network (KB/sec)";
    newRow.appendChild(cell);
}

/*
 * Populates the table identified by id parameter with the specified data and
 * format
 *
 */
function createTable(table, info, formatter, defaultSorts, seriesIndex, headerCreator) {
    var tableRef = table[0];

    // Create header and populate it with data.titles array
    var header = tableRef.createTHead();

    // Call callback is available
    if(headerCreator) {
        headerCreator(header);
    }

    var newRow = header.insertRow(-1);
    for (var index = 0; index < info.titles.length; index++) {
        var cell = document.createElement('th');
        cell.innerHTML = info.titles[index];
        newRow.appendChild(cell);
    }

    var tBody;

    // Create overall body if defined
    if(info.overall){
        tBody = document.createElement('tbody');
        tBody.className = "tablesorter-no-sort";
        tableRef.appendChild(tBody);
        var newRow = tBody.insertRow(-1);
        var data = info.overall.data;
        for(var index=0;index < data.length; index++){
            var cell = newRow.insertCell(-1);
            cell.innerHTML = formatter ? formatter(index, data[index]): data[index];
        }
    }

    // Create regular body
    tBody = document.createElement('tbody');
    tableRef.appendChild(tBody);

    var regexp;
    if(seriesFilter) {
        regexp = new RegExp(seriesFilter, 'i');
    }
    // Populate body with data.items array
    for(var index=0; index < info.items.length; index++){
        var item = info.items[index];
        if((!regexp || filtersOnlySampleSeries && !info.supportsControllersDiscrimination || regexp.test(item.data[seriesIndex]))
                &&
                (!showControllersOnly || !info.supportsControllersDiscrimination || item.isController)){
            if(item.data.length > 0) {
                var newRow = tBody.insertRow(-1);
                for(var col=0; col < item.data.length; col++){
                    var cell = newRow.insertCell(-1);
                    cell.innerHTML = formatter ? formatter(col, item.data[col]) : item.data[col];
                }
            }
        }
    }

    // Add support of columns sort
    table.tablesorter({sortList : defaultSorts});
}

$(document).ready(function() {

    // Customize table sorter default options
    $.extend( $.tablesorter.defaults, {
        theme: 'blue',
        cssInfoBlock: "tablesorter-no-sort",
        widthFixed: true,
        widgets: ['zebra']
    });

    var data = {"OkPercent": 53.42601787487587, "KoPercent": 46.57398212512413};
    var dataset = [
        {
            "label" : "FAIL",
            "data" : data.KoPercent,
            "color" : "#FF6347"
        },
        {
            "label" : "PASS",
            "data" : data.OkPercent,
            "color" : "#9ACD32"
        }];
    $.plot($("#flot-requests-summary"), dataset, {
        series : {
            pie : {
                show : true,
                radius : 1,
                label : {
                    show : true,
                    radius : 3 / 4,
                    formatter : function(label, series) {
                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'
                            + label
                            + '<br/>'
                            + Math.round10(series.percent, -2)
                            + '%</div>';
                    },
                    background : {
                        opacity : 0.5,
                        color : '#000'
                    }
                }
            }
        },
        legend : {
            show : true
        }
    });

    // Creates APDEX table
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.20605759682224428, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [0.0, 500, 1500, "Tea Room"], "isController": false}, {"data": [0.0, 500, 1500, "Login Post"], "isController": false}, {"data": [0.0, 500, 1500, "Article Selection"], "isController": false}, {"data": [1.0, 500, 1500, "Reader"], "isController": false}, {"data": [0.23469387755102042, 500, 1500, "Landing"], "isController": false}, {"data": [0.55, 500, 1500, "Login"], "isController": false}, {"data": [1.0, 500, 1500, "Logout-1"], "isController": false}, {"data": [1.0, 500, 1500, "Logout-0"], "isController": false}, {"data": [0.0, 500, 1500, "Browse"], "isController": false}, {"data": [0.02, 500, 1500, "Edit Article"], "isController": false}, {"data": [0.58, 500, 1500, "Conversation Portal"], "isController": false}, {"data": [0.06, 500, 1500, "Register"], "isController": false}, {"data": [0.08, 500, 1500, "Logout"], "isController": false}, {"data": [0.9, 500, 1500, "Login Post-1"], "isController": false}, {"data": [0.0, 500, 1500, "Login Post-0"], "isController": false}, {"data": [0.18, 500, 1500, "Reply"], "isController": false}, {"data": [0.0, 500, 1500, "Contribute"], "isController": false}, {"data": [0.0, 500, 1500, "Conversation Start"], "isController": false}, {"data": [0.02, 500, 1500, "Manage Contributions"], "isController": false}, {"data": [0.01, 500, 1500, "Library Search"], "isController": false}, {"data": [0.36, 500, 1500, "Library Search Post"], "isController": false}, {"data": [0.0, 500, 1500, "User Management"], "isController": false}]}, function(index, item){
        switch(index){
            case 0:
                item = item.toFixed(3);
                break;
            case 1:
            case 2:
                item = formatDuration(item);
                break;
        }
        return item;
    }, [[0, 0]], 3);

    // Create statistics table
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 1007, 469, 46.57398212512413, 5552.694141012909, 32, 21913, 6138.0, 8658.800000000001, 10310.79999999999, 16083.84, 7.671737987673414, 42.04518157049695, 6.62524831252238], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["Tea Room", 50, 45, 90.0, 12226.7, 7265, 16100, 12080.5, 16088.7, 16097.45, 16100.0, 1.4296743201898607, 1.6595391444828869, 1.5776679509907643], "isController": false}, {"data": ["Login Post", 50, 5, 10.0, 6095.639999999999, 4619, 21913, 4744.0, 12239.099999999988, 21426.399999999998, 21913.0, 2.269117313365101, 26.250629325504878, 3.9089091218516], "isController": false}, {"data": ["Article Selection", 50, 47, 94.0, 7986.839999999998, 4069, 10354, 8116.0, 8802.4, 9677.399999999996, 10354.0, 1.2425756107259127, 0.9120068139740054, 1.3578536214866175], "isController": false}, {"data": ["Reader", 50, 0, 0.0, 390.8799999999999, 218, 497, 383.0, 485.0, 496.45, 497.0, 88.65248226950355, 1021.898202016844, 31.253462987588655], "isController": false}, {"data": ["Landing", 49, 0, 0.0, 2906.571428571429, 150, 7088, 2325.0, 6299.0, 6739.5, 7088.0, 4.93404490987816, 50.334023512234424, 0.6745764525224046], "isController": false}, {"data": ["Login", 50, 0, 0.0, 601.9400000000002, 411, 774, 616.0, 675.0, 724.5999999999997, 774.0, 63.61323155216285, 679.712348918575, 21.9912929389313], "isController": false}, {"data": ["Logout-1", 4, 0, 0.0, 38.0, 32, 46, 37.0, 46.0, 46.0, 46.0, 22.857142857142858, 233.28125000000003, 7.611607142857143], "isController": false}, {"data": ["Logout-0", 4, 0, 0.0, 69.5, 54, 90, 67.0, 90.0, 90.0, 90.0, 18.18181818181818, 11.843039772727273, 21.39559659090909], "isController": false}, {"data": ["Browse", 50, 0, 0.0, 6076.700000000002, 5977, 6176, 6066.0, 6148.6, 6163.0, 6176.0, 8.061915511125443, 74.4573008202999, 2.7949023500483716], "isController": false}, {"data": ["Edit Article", 50, 46, 92.0, 7868.26, 66, 10854, 8076.5, 9555.8, 10115.55, 10854.0, 1.3179745367319502, 1.8101041694124471, 1.4402475650420434], "isController": false}, {"data": ["Conversation Portal", 50, 20, 40.0, 3846.7999999999993, 317, 12080, 403.0, 8402.9, 9533.899999999996, 12080.0, 1.8358729575913346, 11.416906668808519, 1.9990218239397834], "isController": false}, {"data": ["Register", 50, 38, 76.0, 7021.540000000001, 51, 8708, 8058.0, 8496.6, 8591.65, 8708.0, 1.3154779131258387, 4.048331891788787, 1.442657906679997], "isController": false}, {"data": ["Logout", 50, 46, 92.0, 7472.52, 87, 8805, 8199.0, 8562.6, 8727.0, 8805.0, 1.1914690813773383, 1.153723713213392, 1.4338120308590492], "isController": false}, {"data": ["Login Post-1", 50, 5, 10.0, 1086.5, 207, 8719, 271.0, 7352.399999999989, 8501.399999999998, 8719.0, 2.8681236734928013, 30.015306369385648, 3.100598541559112], "isController": false}, {"data": ["Login Post-0", 50, 0, 0.0, 5008.78, 4393, 13195, 4476.0, 4703.6, 13047.1, 13195.0, 3.7545993842457013, 4.143259086130509, 2.4089568314935796], "isController": false}, {"data": ["Reply", 50, 35, 70.0, 7453.080000000001, 137, 15949, 8169.5, 11720.599999999999, 15942.25, 15949.0, 1.43204926249463, 4.9156769117857655, 1.570499337677216], "isController": false}, {"data": ["Contribute", 50, 44, 88.0, 7835.159999999999, 4038, 11707, 8098.0, 8936.3, 10090.15, 11707.0, 1.3748728242637556, 2.037335960486155, 1.5118230469931533], "isController": false}, {"data": ["Conversation Start", 50, 44, 88.0, 7633.900000000001, 4085, 8778, 8081.5, 8183.8, 8199.65, 8778.0, 1.2420200213627444, 2.123320556052364, 1.3596723085426137], "isController": false}, {"data": ["Manage Contributions", 50, 47, 94.0, 7638.879999999998, 54, 9561, 8061.0, 8192.9, 8403.499999999998, 9561.0, 1.3162051174054965, 0.9758684074576182, 1.4383139906022955], "isController": false}, {"data": ["Library Search", 50, 0, 0.0, 3460.4200000000005, 1497, 9310, 1601.0, 7308.6, 9263.6, 9310.0, 4.2066296483257615, 47.0300373075467, 0.6038814045936396], "isController": false}, {"data": ["Library Search Post", 50, 0, 0.0, 1354.8000000000002, 291, 2608, 961.5, 2536.1, 2599.5, 2608.0, 17.024174327545115, 196.22921188712974, 6.068187138236296], "isController": false}, {"data": ["User Management", 50, 47, 94.0, 7914.88, 4119, 10246, 8088.0, 8213.0, 9217.0, 10246.0, 1.2423286207667652, 1.6425913732700574, 1.3490912366139092], "isController": false}]}, function(index, item){
        switch(index){
            // Errors pct
            case 3:
                item = item.toFixed(2) + '%';
                break;
            // Mean
            case 4:
            // Mean
            case 7:
            // Median
            case 8:
            // Percentile 1
            case 9:
            // Percentile 2
            case 10:
            // Percentile 3
            case 11:
            // Throughput
            case 12:
            // Kbytes/s
            case 13:
            // Sent Kbytes/s
                item = item.toFixed(2);
                break;
        }
        return item;
    }, [[0, 0]], 0, summaryTableHeader);

    // Create error table
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["500/Internal Server Error", 469, 100.0, 46.57398212512413], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 1007, 469, "500/Internal Server Error", 469, "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": ["Tea Room", 50, 45, "500/Internal Server Error", 45, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Login Post", 50, 5, "500/Internal Server Error", 5, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Article Selection", 50, 47, "500/Internal Server Error", 47, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Edit Article", 50, 46, "500/Internal Server Error", 46, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Conversation Portal", 50, 20, "500/Internal Server Error", 20, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Register", 50, 38, "500/Internal Server Error", 38, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Logout", 50, 46, "500/Internal Server Error", 46, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Login Post-1", 50, 5, "500/Internal Server Error", 5, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": ["Reply", 50, 35, "500/Internal Server Error", 35, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Contribute", 50, 44, "500/Internal Server Error", 44, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Conversation Start", 50, 44, "500/Internal Server Error", 44, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Manage Contributions", 50, 47, "500/Internal Server Error", 47, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["User Management", 50, 47, "500/Internal Server Error", 47, "", "", "", "", "", "", "", ""], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
