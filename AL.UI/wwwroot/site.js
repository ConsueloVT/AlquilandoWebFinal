// wwwroot/site.js
window.initDropdowns = () => {
    var dropdownElementList = [].slice.call(document.querySelectorAll('.dropdown-toggle'));
    dropdownElementList.map(function (dropdownToggleEl) {
        new bootstrap.Dropdown(dropdownToggleEl);
    });
};