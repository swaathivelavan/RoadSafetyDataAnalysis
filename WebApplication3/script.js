
// Our labels along the x-axis
var years = [1500,1600,1700,1750,1800,1850,1900,1950,1999,2050];
// For drawing the lines
var africa = [86,114,106,106,107,111,133,221,783,2478];
var asia = [282,350,411,502,635,809,947,1402,3700,5267];
var europe = [168,170,178,190,203,276,408,547,675,734];
var latinAmerica = [40,20,10,16,24,38,74,167,508,784];
var northAmerica = [6,3,2,2,7,26,82,172,312,433];

var ctx = document.getElementById("chartHours");
var chartHours = new Chart(ctx, {
  type: 'line',
  data: {
    labels: years,
    datasets: [
      { 
        data: africa,
        label: "Africa",
        borderColor: "#3e95cd",
        fill: false
      },
      { 
        data: asia,
        label: "Asia",
        borderColor: "#8e5ea2",
        fill: false
      },
      { 
        data: europe,
        label: "Europe",
        borderColor: "#3cba9f",
        fill: false
      },
      { 
        data: latinAmerica,
        label: "Latin America",
        borderColor: "#e8c3b9",
        fill: false
      },
      { 
        data: northAmerica,
        label: "North America",
        borderColor: "#c45850",
        fill: false
      }
    ]
  }
});

$.ajax({

    url: "WebService1.asmx/GetAllSites",
    method: 'post',
    dataType: "json",

    //contentType: "application/json; charset=utf-8",


    success: function (result) {

        console.log("success");

        var data = [];
        var labels = [];
        var borderColor = [];
        var backgroundColor = [];
        var red, green, blue;

        const ctx = document.getElementById('myChart').getContext('2d');

        if (result) {

            for (var i = 0; i < result.length; i++) {
                if (result[i] && result[i].noofaccidents) {
                    data[i] = result[i].noofaccidents;
                    labels[i] = result[i].year;
                }
                red = Math.floor((Math.random() * 255) + 1);
                green = Math.floor((Math.random() * 255) + 1);
                blue = Math.floor((Math.random() * 255) + 1);


                backgroundColor[i] = 'rgba(' + red + ',' + green + ','
                    + blue + ', 0.2)';
                borderColor[i] = 'rgba(' + red + ',' + green + ',' + blue
                    + ', 0.2)';
            }
        }
        const myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: '# of Votes',
                    data: data,
                    backgroundColor: backgroundColor,
                    borderColor: borderColor,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });


        //$("#container").append(table);
    },
    error: function (xhr, err) {
        console.log(xhr.responseText);
        alert("error");
    }

});