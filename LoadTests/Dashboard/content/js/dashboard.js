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

    var data = {"OkPercent": 100.0, "KoPercent": 0.0};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.9628392199645438, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [0.998, 500, 1500, "Tea Room"], "isController": false}, {"data": [0.5805, 500, 1500, "Login Post"], "isController": false}, {"data": [0.9985, 500, 1500, "Article Selection"], "isController": false}, {"data": [0.9975, 500, 1500, "Reader"], "isController": false}, {"data": [0.9994994994994995, 500, 1500, "Landing"], "isController": false}, {"data": [1.0, 500, 1500, "Login"], "isController": false}, {"data": [1.0, 500, 1500, "Logout-1"], "isController": false}, {"data": [1.0, 500, 1500, "Logout-0"], "isController": false}, {"data": [0.9965, 500, 1500, "Browse"], "isController": false}, {"data": [0.995, 500, 1500, "Edit Article"], "isController": false}, {"data": [1.0, 500, 1500, "Conversation Portal"], "isController": false}, {"data": [1.0, 500, 1500, "Register"], "isController": false}, {"data": [0.999, 500, 1500, "Logout"], "isController": false}, {"data": [1.0, 500, 1500, "Login Post-1"], "isController": false}, {"data": [0.765, 500, 1500, "Login Post-0"], "isController": false}, {"data": [1.0, 500, 1500, "Reply"], "isController": false}, {"data": [1.0, 500, 1500, "Contribute"], "isController": false}, {"data": [0.99, 500, 1500, "Conversation Start"], "isController": false}, {"data": [0.869, 500, 1500, "Manage Contributions"], "isController": false}, {"data": [0.9965, 500, 1500, "Library Search"], "isController": false}, {"data": [0.9975, 500, 1500, "Library Search Post"], "isController": false}, {"data": [1.0, 500, 1500, "User Management"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 21999, 0, 0.0, 231.76153461521008, 0, 1219, 194.0, 440.0, 573.9000000000015, 789.9900000000016, 481.3890894767938, 3382.780360347054, 413.1715648865402], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["Tea Room", 1000, 0, 0.0, 222.04900000000032, 62, 630, 215.0, 304.0, 335.89999999999986, 454.0, 22.769707181565643, 150.33788110797394, 24.593062639464456], "isController": false}, {"data": ["Login Post", 1000, 0, 0.0, 654.4479999999991, 231, 1219, 654.5, 868.8, 941.8499999999998, 1025.99, 22.607044355021028, 192.27026297644346, 37.884461048062576], "isController": false}, {"data": ["Article Selection", 1000, 0, 0.0, 207.9339999999999, 49, 683, 207.0, 291.0, 320.0, 360.0, 22.932623950832454, 148.34541118194744, 24.522678931798374], "isController": false}, {"data": ["Reader", 1000, 0, 0.0, 222.24599999999998, 32, 669, 217.0, 314.9, 350.8499999999998, 455.99, 22.769707181565643, 165.10261310852042, 7.493546211120725], "isController": false}, {"data": ["Landing", 999, 0, 0.0, 107.05505505505488, 4, 514, 93.0, 171.0, 235.0, 384.0, 22.555372423291413, 133.65820299270734, 6.543725023142399], "isController": false}, {"data": ["Login", 1000, 0, 0.0, 152.66200000000023, 41, 327, 148.0, 220.0, 240.89999999999986, 285.0, 22.697353488583232, 145.4714169390349, 7.3145768078442055], "isController": false}, {"data": ["Logout-1", 1000, 0, 0.0, 85.54500000000006, 0, 263, 82.0, 147.0, 156.0, 195.0, 23.31219694143976, 138.1429795318911, 7.216764092223051], "isController": false}, {"data": ["Logout-0", 1000, 0, 0.0, 156.64700000000025, 6, 449, 153.0, 228.89999999999998, 257.94999999999993, 309.97, 23.254732338030788, 12.603883249616295, 26.820155167201523], "isController": false}, {"data": ["Browse", 1000, 0, 0.0, 221.25199999999998, 34, 704, 213.5, 302.0, 344.0, 440.6800000000003, 22.72830583208328, 112.73150910268649, 7.346747295331606], "isController": false}, {"data": ["Edit Article", 1000, 0, 0.0, 268.9440000000001, 26, 542, 269.0, 379.9, 417.89999999999986, 500.99, 22.97160709363227, 271.1950763231646, 24.564365007350915], "isController": false}, {"data": ["Conversation Portal", 1000, 0, 0.0, 155.25899999999984, 35, 438, 150.0, 224.0, 250.0, 332.9000000000001, 22.75830678197542, 146.0843266385981, 24.247375682749205], "isController": false}, {"data": ["Register", 1000, 0, 0.0, 153.91799999999986, 10, 452, 147.0, 227.0, 252.0, 315.99, 23.158869847151458, 193.84245455071792, 24.85507613478462], "isController": false}, {"data": ["Logout", 1000, 0, 0.0, 242.43600000000018, 6, 643, 240.0, 334.0, 351.94999999999993, 453.95000000000005, 23.254732338030788, 150.4063401120878, 34.01912992418957], "isController": false}, {"data": ["Login Post-1", 1000, 0, 0.0, 158.02000000000007, 27, 450, 147.0, 229.79999999999995, 262.0, 374.99, 22.768670309653913, 171.00961264799636, 24.08053705601093], "isController": false}, {"data": ["Login Post-0", 1000, 0, 0.0, 496.1999999999998, 171, 1045, 490.0, 699.8, 765.7499999999997, 880.99, 22.648004710784978, 22.515301558182724, 14.000182599537979], "isController": false}, {"data": ["Reply", 1000, 0, 0.0, 156.85799999999995, 26, 452, 148.0, 233.0, 259.0, 320.98, 22.839393385711674, 161.77160178375664, 24.51220051845423], "isController": false}, {"data": ["Contribute", 1000, 0, 0.0, 151.1939999999999, 43, 397, 148.0, 222.0, 250.0, 323.99, 22.85139736294874, 170.2473735175156, 24.592031146454605], "isController": false}, {"data": ["Conversation Start", 1000, 0, 0.0, 265.60999999999984, 54, 758, 260.0, 380.69999999999993, 442.89999999999986, 520.98, 22.872827081427268, 213.6286310612992, 24.503409480786825], "isController": false}, {"data": ["Manage Contributions", 1000, 0, 0.0, 409.166, 26, 924, 404.0, 609.9, 662.9499999999999, 768.98, 23.016548898658137, 374.0863508987962, 24.61242289456119], "isController": false}, {"data": ["Library Search", 1000, 0, 0.0, 237.90199999999993, 56, 758, 228.0, 342.9, 388.0, 487.94000000000005, 22.64697889301567, 152.88701209914848, 6.721110239831507], "isController": false}, {"data": ["Library Search Post", 1000, 0, 0.0, 219.81799999999987, 43, 697, 215.0, 306.0, 341.94999999999993, 442.0, 22.73037232349866, 164.81739697458744, 7.56939156475883], "isController": false}, {"data": ["User Management", 1000, 0, 0.0, 153.4659999999998, 34, 472, 146.0, 224.89999999999998, 250.0, 294.96000000000004, 22.940515243972378, 144.76719678373976, 24.374297446720654], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": []}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 21999, 0, "", "", "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
