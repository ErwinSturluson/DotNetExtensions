window.onload = function() {
    console.log("[layout.js.onload]: was loaded.");
};

document.addEventListener("DOMContentLoaded", setupEventHandlers);

function setupEventHandlers() {
    setupSidebarButtons();
    setupScrollContainers();
}

function setupSidebarButtons() {

    var button = document.querySelector('#sidebar-button');
    var sidebar = document.querySelector('#sidebar');

    button.onclick = () => {
        var display = getComputedStyle(sidebar).getPropertyValue("display");

        if (display == 'none') {
            sidebar.style.display = 'flex';
            var container = document.querySelector('#scroll-container');
            var content = document.querySelector('#scroll-content');
            container.style.width = `${content.scrollWidth}px`;
        }
        else {
            sidebar.style.display = 'none';
        }
    };
}

function setupScrollContainers() {
    var containers = document.querySelectorAll('.scroll-container');

    containers.forEach(function(container) {
        var content = container.querySelector('.scroll-content');
        if (content) {
            container.style.width = `${content.scrollWidth}px`;

            window.addEventListener('resize', function() {
                container.style.width = `${content.scrollWidth}px`;
            });
        }
    });
}