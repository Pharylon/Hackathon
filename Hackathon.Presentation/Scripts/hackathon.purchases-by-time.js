/// <reference path="~/Scripts/jquery-2.0.0.js" />
/// <reference path="~/Scripts/d3.v3.js" />
///  <reference path="~/Scripts/select2.js" />
var margin = { top: 20, right: 20, bottom: 30, left: 40 },
    width = 960 - margin.left - margin.right,
    height = 500 - margin.top - margin.bottom;

var x = d3.scale.linear()
    .range([0, width]);

var y = d3.scale.linear()
    .range([height, 0]);

var color = d3.scale.category10();

var xAxis = d3.svg.axis()
    .scale(x)
    .orient("bottom");

var yAxis = d3.svg.axis()
    .scale(y)
    .orient("left");

var svg = d3.select("#graph").append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
  .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

d3.json("PurchasesByTime/GetData", function (error, data) { buildGraph(error, data) });
function buildGraph(error,data){
  data.forEach(function(d) {
    d.Item1 = +d.Item1;
    d.Item2 = +d.Item2;
  });

    //Item1 is y, Item2 is x
    //purchas amount is y, time is x
  x.domain(d3.extent(data, function(d) { return d.Item2; })).nice();
  y.domain(d3.extent(data, function(d) { return d.Item1; })).nice();

  svg.append("g")
      .attr("class", "x axis")
      .attr("transform", "translate(0," + height + ")")
      .call(xAxis)
    .append("text")
      .attr("class", "label")
      .attr("x", width)
      .attr("y", -6)
      .style("text-anchor", "end")
      .text("Time of Day");

  svg.append("g")
      .attr("class", "y axis")
      .call(yAxis)
    .append("text")
      .attr("class", "label")
      .attr("transform", "rotate(-90)")
      .attr("y", 6)
      .attr("dy", ".71em")
      .style("text-anchor", "end")
      .text("Money Spent ($)")

  svg.selectAll(".dot")
      .data(data)
    .enter().append("circle")
      .attr("class", "dot")
      .attr("r", 3.5)
      .attr("cx", function(d) { return x(d.Item2); })
      .attr("cy", function(d) { return y(d.Item1); })
      //.style("fill", function(d) { return color(d.Item3); });

  var legend = svg.selectAll(".legend")
      .data(color.domain())
    .enter().append("g")
      .attr("class", "legend")
      .attr("transform", function(d, i) { return "translate(0," + i * 20 + ")"; });

  legend.append("rect")
      .attr("x", width - 18)
      .attr("width", 18)
      .attr("height", 18)
      .style("fill", color);

  legend.append("text")
      .attr("x", width - 24)
      .attr("y", 9)
      .attr("dy", ".35em")
      .style("text-anchor", "end")
      .text(function(d) { return d; });

};

//selet 2

var filterData = [
  { id: 'description', text: 'Purchase Category', locked: true}
  , { id: 'store', text: 'Store'}
  , { id: 'income', text: 'Household Income' }
  , { id: 'outlaymin', text: 'Outlay Less Than' }
  , { id: 'outlaymax', text: 'Outlay More Than' }
];
var descriptionData = [
  { id: 'bakery', text: 'Bakery' }
  , { id: 'beer', text: 'Beer'}
  , { id: 'deli', text: 'Deli' }
  , { id: 'wine', text: 'Wine' }
];

var storeData = [
  { id: 'store1', text: 'Store 1' }
  , { id: 'store2', text: 'Store 2' }
  , { id: 'store3', text: 'Store 3' }
  , { id: 'store4', text: 'Store 4' }
  , { id: 'store5', text: 'Store 5' }
];

var incomeData = [
  { id: 'lt30000', text: 'Less Than 30,000' }
  , { id: 'lt40000', text: 'Less Than 40,000' }
  , { id: 'mt70000', text: 'More Than 70,000' }
  , { id: 'mt10000', text: 'More Than 100,000' }
];

var outlayData = [
    { id: '1', text: '.1' }
  , { id: '2', text: '.2' }
  , { id: '3', text: '.3' }
  , { id: '4', text: '.4' }
  , { id: '5', text: '.5' }
]
 
function InitializeFilterSelector()
{
    $("#filter-selector").empty();
    for (var i = 0; i < filterData.length;i++)
    {
        $("#filter-selector").append($("<option>",
            {
                "value": filterData[i].id,
                text: filterData[i].text
            }));
    }
    $("#filter-selector")
            .on("change", function (data) {
                if ($("#filter-selector").val() == 'store') {
                    SetChoiceSelector(storeData);
                }
                else if ($("#filter-selector").val() == 'description')
                    SetChoiceSelector(descriptionData);
                else if ($("#filter-selector").val() == 'income')
                    SetChoiceSelector(incomeData);
                else if ($("#filter-selector").val() == 'outlaymin' || 'outlaymax')
                    SetChoiceSelector(outlayData);
            });
    SetChoiceSelector(descriptionData);
}
function SetChoiceSelector(data)
{
    $("#filter-choice-selector").empty();
    for (var i = 0; i < data.length; i++) {
        $("#filter-choice-selector").append($("<option>",
                {
                    "value": data[i].id,
                    text: data[i].text
                }));
    }
}

function addFilter() {
    var filtercategory = $("#filter-selector").val();
    var filterchoice = $("#filter-choice-selector").val();
    var filterDescription = $("#filter-selector option:selected").text() + ": " + $("#filter-choice-selector option:selected").text();
    var filterTag = $("<span>", {
            "class": "tag",
            "data-filtercategory": filtercategory,
            "data-filterchoice": filterchoice,
            text: filterDescription
    }).append($("<button>", { "class": "close", text: x }).on("click", function(e){$(e.currentTarget).parent().remove()}));
    $("#filters").append(filterTag);
}

function getFilterData() {
    var filters = [];
    $("#filters").children("span").each(function (index, element) {
        var filtercategory = $(element).data("filtercategory");
        var filterchoice = $(element).data("filterchoice");
        var filter = { "filtercategory": filtercategory, "filterchoice": filterchoice }
        filters.push(filter);
    });
    return filters;
}
function  executeFiltering()
{
    var url = "PurchasesByTime/GetFilteredData";
    data = "filters=" + JSON.stringify(getFilterData());
        $("#graph").html("");
        svg = d3.select("#graph").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
        $.get(url, data, function (data) {
            var error = null;
            buildGraph(error, data);
        });
    //d3.json(url, function (error, data) { buildGraph(error, data) });
}

$(document).ready(function () {

    InitializeFilterSelector()
    
    $('#filter-selector').on("change", function (data) {
        if (data.val == 'store'){
            
        }
        else if (data.val == 'description')
            $('#filter-choice-selector').select2('data', { data: descriptionData });
        else if(data.val == 'income')
            $('#filter-choice-selector').select2('data',incomeData)
    });

    $("#addFilter").on("click", function () {
        addFilter();
    });
    $("#execute-button").on("click", function () {
        executeFiltering();
    });

});