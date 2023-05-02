var loader = document.getElementById('preloader');
window.addEventListener("load", function () {
    loader.style.display = "none";
});
$(document).ready(function () {
    filter();
    
});
function filter(pg, sortValue) {
    check();

    var Search = $("input[name='searchinput']").val();

    console.log(Search)

  
    var country = [];
 

  
     
    $("input[type='checkbox'][name='country']:checked").each(function () {
        country.push($(this).val());
      
    });
    console.log(country)


    var city = [];
    $("input[type='checkbox'][name='city']:checked").each(function () {
        city.push($(this).val());
    });
    console.log(city)

    var theme = [];
    $("input[type='checkbox'][name='theme']:checked").each(function () {
        theme.push($(this).val());
    });
    console.log(theme)

    $.ajax({
        url: "/Employee/Landingpage/Filters",
        type: "POST",
        data: { 'search': Search, 'sortValue': sortValue, 'country': country, 'city': city, 'theme': theme, 'pg': pg },

        success: function (res) {
            $("#missions").html('');
            $("#missions").html(res);

        },
        error: function () {
            alert("some Error");
        }
    });
}


/*chackbox */
var checkboxes = document.querySelectorAll(".checkboxf");

let filtersSection = document.querySelector(".filters-section");

var listArray = [];

var filterList = document.querySelector(".filter-list");

var len = listArray.length;

for (var checkbox of checkboxes) {
    checkbox.addEventListener("click", function () {
        if (this.checked == true) {
            addElement(this, this.value);
            clear()
        }
        else {
            removeElement(this.value);
            console.log("unchecked");
        }
    })
}

function addElement(current, value) {
    let filtersSection = document.querySelector(".filters-section");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('ps-3');
    createdTag.classList.add('pe-1');
    createdTag.classList.add('me-2');
    //createdTag.classList.add('border border-dark');
    createdTag.innerHTML = value;

    //let createdTag = document.createElement('span');
    //createdTag.classList.add('filter-list');
    //createdTag.classList.add('ps-3');
    //createdTag.classList.add('pe-1');
    //createdTag.classList.add('me-2');
    ////createdTag.classList.add('border border-dark');
    //createdTag.innerHTML = "Close all";

    createdTag.setAttribute('id', value);
    createdTag.setAttribute('name', value);
    let crossButton = document.createElement('button');
    crossButton.classList.add("filter-close-button");
    let cross = '&times';

    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        elementToBeRemoved.remove();
        console.log(current);
        current.checked = false;
        filter();
    })

    crossButton.innerHTML = cross;

    // let crossButton = '&times;'

    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);
    filter();

}

function removeElement(value) {

    let filtersSection = document.querySelector(".filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);
    filter();
}
function clear() {

    $(".clear-all").remove();

    let filtersSection = document.querySelector(".filters-section");

    let clearAll = document.createElement('span');
    clearAll.classList.add('clear-all');
  
    clearAll.innerHTML = "Clear All";
    clearAll.classList.add('filter-list');
    clearAll.classList.add('ps-3');
    clearAll.classList.add('pe-3');
    clearAll.classList.add('py-1');
    clearAll.classList.add('me-2');
    clearAll.setAttribute("onclick", "ClearAll()")

    filtersSection.appendChild(clearAll);
}

function check() {
    var numberOfChecked = $('input:checkbox:checked').length;
    if (numberOfChecked <= 0) {
        $(".clear-all").remove();
    }
}


function ClearAll() {
    $(".filter-close-button").click()
    $(".clear-all").remove();
}