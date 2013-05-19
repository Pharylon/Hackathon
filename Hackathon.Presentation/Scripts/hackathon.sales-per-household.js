
var margin = { top: 20, right: 20, bottom: 30, left: 50 },
    width = 960 - margin.left - margin.right,
    height = 500 - margin.top - margin.bottom;

var x = d3.scale.linear()
    .range([0, width]);

var y = d3.scale.log()
    .range([height, 0]);

var xAxis = d3.svg.axis()
    .scale(x)
    .orient("bottom");

var yAxis = d3.svg.axis()
    .scale(y)
    .orient("left");

var line = d3.svg.line()
    .x(function (d) { return x(d.Item1); })
    .y(function (d) { return y(d.Item2); });

var svg = d3.select("#graph").append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
  .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

var formatNumber = d3.format(",.0f"), // for formatting integers
    formatCurrency = function(d) { return "$" + formatNumber(d); };


d3.json("SalesPerHousehold/ByDescription?description=Deli", function (error, data) {
	data.forEach(function (d) {
	});

	x.domain(d3.extent(data, function (d) { return d.Item1; }));
	y.domain(d3.extent(data, function (d) { return d.Item2; }));

	svg.append("g")
		.attr("class", "x axis")
		.attr("transform", "translate(0," + height + ")")
		.call(xAxis);

	svg.append("g")
		.attr("class", "y axis")
		.call(yAxis)
	  .append("text")
		.attr("transform", "rotate(-90)")
		.attr("y", 6)
		.attr("dy", ".71em")
		.style("text-anchor", "end")
		.text("Price ($)");

	svg.append("path")
		.datum(data)
		.attr("class", "line")
		.attr("d", line);
});
