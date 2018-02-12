

function sideBarFun() {
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#dismiss, .overlay').on('click', function () {
        $('#sidebar').removeClass('active');
        $('.overlay').fadeOut();
    });

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $('.overlay').fadeIn();
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
}

function convertToSlug(Text) {
    return Text
        .trim()
        .toLowerCase()
        .replace(/\s\s+/g, ' ')
        .replace(/ /g, '-')
        .replace(/[^\w-]+/g, '');
}

function getTextAfterLastSlash() {
    return window.location.href.substring(window.location.href.lastIndexOf('/') + 1);
}

function fetchAllFacts(url) {
    console.log('Making fetch() request to: ' + url);
    fetch(url)
        .then(getJson)
        .then(constructFactTemplate)
        .catch(catchError);
}

function getJson(response) {
    console.log(response.headers.get("x-pagination"));
    return response.json();
}

function constructFactTemplate(json) {
    var myHelpers = { format: convertToSlug };
    console.log("fetchDemo: " + json);
    var tmpl = $.templates("#myTmpl"); // Get compiled template
    var data = { testData: json };           // Define data
    var html = tmpl.render(data, myHelpers);      // Render template using data - as HTML string
    $(".cards").html(html);           // Insert HTML string into DOM
}

function catchError(resonse) {
    console.log("catchError: " + resonse);
    debugger;
}

function fetchAllCategories(url) {
    console.log('Making fetch() request to: ' + url);
    fetch(url)
        .then(getJson)
        .then(constructCategoryTemplate)
        .catch(catchError);
}

function constructCategoryTemplate(json) {
    console.log("fetchDemo: " + json);
    var tmpl = $.templates("#categoriesTmpl"); // Get compiled template
    var data = { categoriesData: json };           // Define data
    var html = tmpl.render(data);      // Render template using data - as HTML string
    $(".categories").html(html);           // Insert HTML string into DOM

}

function fetchAllTags(url) {
    console.log('Making fetch() request to: ' + url);
    fetch(url)
        .then(getJson)
        .then(constructTagsTemplate)
        .catch(catchError);
}

function constructTagsTemplate(json) {
    console.log("fetchDemo: " + json);
    var tmpl = $.templates("#tagsTmpl"); // Get compiled template
    var data = { tagsData: json };           // Define data
    var html = tmpl.render(data);      // Render template using data - as HTML string
    $(".tags").html(html);           // Insert HTML string into DOM

}

function fetchSingleCategory(url) {
    console.log('Making fetch() request to: ' + url);
    fetch(url)
        .then(getJson)
        .then(constructCategoryTitleTemplate)
        .catch(catchError);
}

function constructCategoryTitleTemplate(json) {
    console.log("fetchDemo: " + json);
    var tmpl = $.templates("#categoryTitleTmpl"); // Get compiled template
    var html = tmpl.render(json);      // Render template using data - as HTML string
    $(".categoryTitle").html(html);           // Insert HTML string into DOM
}

function fetchSinglePost(url) {
    console.log('Making fetch() request to: ' + url);
    fetch(url)
        .then(getJson)
        .then(constructSinglePostTemplate)
        .catch(catchError);
}

function constructSinglePostTemplate(json) {
    console.log("fetchDemo: " + json);
    var tmpl = $.templates("#singleTmpl"); // Get compiled template
    var html = tmpl.render(json);      // Render template using data - as HTML string
    $(".card").html(html);           // Insert HTML string into DOM
}
