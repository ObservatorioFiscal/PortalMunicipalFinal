
function drawChart(data, diameterWidth, diameterHeigth) {

    var localeFormatter = d3.formatLocale({
        "decimal": ",",
        "thousands": "."
    });

    //format = localeFormatter.format(",.0f"), //ya no se usa si datos editar un html
    //color = d3.scaleOrdinal(d3.schemeCategory20c);

    var bubble = d3.pack()
        .size([diameterHeigth, diameterHeigth])
        .padding(1.5);

    var svg = d3.select("#chart").append("svg")
        .attr("width", diameterWidth)
        .attr("height", diameterHeigth)
        .attr("class", "bubble");

    var lateralPanel = d3.select("#lateralPanel").append("ul");

    var minColor = "#d5ebf7";
    var maxColor = "#285685";
    var colorlist = [minColor, maxColor];
    //var functionlist = [d3.interpolateRgb, d3.interpolateHsl, d3.interpolateHcl];

    var limitRange = data.children.length;
    var vectorColor = (
        d3.scaleLinear()
            .domain([0, limitRange])
            .interpolate(d3.interpolateRgb)
            .range([colorlist[0], colorlist[1]])
    );

    var root = d3.hierarchy(classes(data))
        .sum(function (d) { return d.value; })
        .sort(function (a, b) { return b.value - a.value; });

    bubble(root);
    var node = svg.selectAll(".node")
        .data(root.children)
        .enter().append("g")
        .attr("class", "node")
        .attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; })
        .on("mouseover", function (d) {
            var tooltip = d3.select("#tooltip");
            tooltip.style("left", (d3.event.pageX + 10) + "px");
            tooltip.style("top", (d3.event.pageY - 160) + "px");
            document.getElementById("clasificacion").innerHTML = "$ " + d.data.value.toLocaleString();
            document.getElementById("monto").innerHTML = d.data.className;
            tooltip.style("opacity", "1")
        })
        .on("mousemove", function (d) {
            //d3.select("#tooltip").classed("hidden", true);
            var tooltip = d3.select("#tooltip");
            tooltip.style("left", (d3.event.pageX + 20) + "px");
            tooltip.style("top", (d3.event.pageY - 110) + "px");


            
            
        })
        .on("mouseout", function () {
            var tooltip = d3.select("#tooltip");
            tooltip.style("opacity", "0")

        });

    node.append("circle")
        .attr("r", function (d) { return d.r; })
        .attr("class", "circle")
        .attr("onclick", function (d) { return "selectCircle('" + d.data.className.replace(/ /g, '') + "','" + d.data.className + "')" })
        .attr("id", function (d) { return d.data.className.replace(/ /g, '') + "B" })
        .style("fill", function (d) {
            var colorPosition = d.data.orderDescPosition;
            var div = lateralPanel.append("div")
            div.attr("onclick", "selectCircle('" + d.data.className.replace(/ /g, '') + "','" + d.data.className + "')");

            var li = div.append("li").attr("style", "list-style-type: none");
            li.append("i")
                .attr("class", "circle fa fa-circle")
                .attr("id", d.data.className.replace(/ /g, '') + "S")
                .attr("style", "color:" + vectorColor(colorPosition));
            li.append("label").text(d.data.className);
            return vectorColor(colorPosition);
        });

    //node.append("input")
    //    .attr("type", "hidden")
    //    .attr("id", function (d) { return d.data.className.replace(/ /g, '') + "i" })
    //    .attr("value", function (d) { return d.data.className })
    //    .text();

    d3.select(self.frameElement).style("height", diameterHeigth + "px");
}
// Returns a flattened hierarchy containing all leaf nodes under the root.
function classes(root) {
    var classes = [];

    function recurse(name, node) {
        if (node.children) node.children.forEach(function (child) { recurse(node.name, child); });
        else classes.push({ packageName: name, className: node.name, value: node.size, orderDescPosition: node.position });
    }

    recurse(null, root);
    return { children: classes };
}


function selectCircle(idSelected, nameSelected) {
    deselectCircle();
    var circles = document.getElementsByClassName("circle");
    $("#buscaSubsidio").val('');
    $("#buscaSubsidio").val(nameSelected);
    $("#buscaSubsidio").change();
    for (i = 0; i < circles.length; i++) {
        if (circles[i].id != (idSelected + "S") && circles[i].id != (idSelected + "B")) {
            circles[i].classList.add("disable-color");
        }
    }
}

function deselectCircle() {
    var circles = document.getElementsByClassName("circle");
    for (i = 0; i < circles.length; i++) {
        circles[i].classList.remove("disable-color");
    }
}
function drawChart(data, diameterWidth, diameterHeigth) {

    var localeFormatter = d3.formatLocale({
        "decimal": ",",
        "thousands": "."
    });

    //format = localeFormatter.format(",.0f"), //ya no se usa si datos editar un html
    //color = d3.scaleOrdinal(d3.schemeCategory20c);

    var bubble = d3.pack()
        .size([diameterHeigth, diameterHeigth])
        .padding(1.5);

    var svg = d3.select("#chart").append("svg")
        .attr("width", diameterWidth)
        .attr("height", diameterHeigth)
        .attr("class", "bubble");

    var lateralPanel = d3.select("#lateralPanel").append("ul");

    var minColor = "#d5ebf7";
    var maxColor = "#285685";
    var colorlist = [minColor, maxColor];
    //var functionlist = [d3.interpolateRgb, d3.interpolateHsl, d3.interpolateHcl];

    var limitRange = data.children.length;
    var vectorColor = (
        d3.scaleLinear()
            .domain([0, limitRange])
            .interpolate(d3.interpolateRgb)
            .range([colorlist[0], colorlist[1]])
    );

    var root = d3.hierarchy(classes(data))
        .sum(function (d) { return d.value; })
        .sort(function (a, b) { return b.value - a.value; });

    bubble(root);
    var node = svg.selectAll(".node")
        .data(root.children)
        .enter().append("g")
        .attr("class", "node")
        .attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; })
        .on("mouseover", function (d) {

            var tooltip = d3.select("#tooltip");
            tooltip.style("left", (d3.event.pageX + 10) + "px");
            tooltip.style("top", (d3.event.pageY - 160) + "px");
            document.getElementById("clasificacion").innerHTML = "$ " + d.data.value.toLocaleString();
            document.getElementById("monto").innerHTML = d.data.className;
            tooltip.style("opacity", "1")
        })
        .on("mousemove", function (d) {
            var tooltip = d3.select("#tooltip");
            tooltip.style("left", (d3.event.pageX + 20) + "px");
            tooltip.style("top", (d3.event.pageY - 110) + "px");
        })
        .on("mouseout", function () {
            var tooltip = d3.select("#tooltip");
            tooltip.style("opacity", "0")

        });

    node.append("circle")
        .attr("r", function (d) { return d.r; })
        .attr("class", "circle")
        .attr("onclick", function (d) { return "selectCircle('" + d.data.className.replace(/ /g, '') + "','" + d.data.className + "')" })
        .attr("id", function (d) { return d.data.className.replace(/ /g, '') + "B" })
        .style("fill", function (d) {
            var colorPosition = d.data.orderDescPosition;
            var div = lateralPanel.append("div")
            div.attr("onclick", "selectCircle('" + d.data.className.replace(/ /g, '') + "','" + d.data.className + "')");

            var li = div.append("li").attr("style", "list-style-type: none");
            li.append("i")
                .attr("class", "circle")
                .attr("id", d.data.className.replace(/ /g, '') + "S")
                .attr("style", "background-color:" + vectorColor(colorPosition));
            li.append("label").text(d.data.className);
            return vectorColor(colorPosition);
        });

    //node.append("input")
    //    .attr("type", "hidden")
    //    .attr("id", function (d) { return d.data.className.replace(/ /g, '') + "i" })
    //    .attr("value", function (d) { return d.data.className })
    //    .text();

    d3.select(self.frameElement).style("height", diameterHeigth + "px");
}
// Returns a flattened hierarchy containing all leaf nodes under the root.
function classes(root) {
    var classes = [];

    function recurse(name, node) {
        if (node.children) node.children.forEach(function (child) { recurse(node.name, child); });
        else classes.push({ packageName: name, className: node.name, value: node.size, orderDescPosition: node.position });
    }

    recurse(null, root);
    return { children: classes };
}


function selectCircle(idSelected, nameSelected) {
    deselectCircle();
    var circles = document.getElementsByClassName("circle");
    $("#buscaSubsidio").val('');
    $("#buscaSubsidio").val(nameSelected);
    $("#buscaSubsidio").change();
    for (i = 0; i < circles.length; i++) {
        if (circles[i].id != (idSelected + "S") && circles[i].id != (idSelected + "B")) {
            circles[i].classList.add("disable-color");
        }
    }
}

function deselectCircle() {
    var circles = document.getElementsByClassName("circle");
    for (i = 0; i < circles.length; i++) {
        circles[i].classList.remove("disable-color");
    }
}