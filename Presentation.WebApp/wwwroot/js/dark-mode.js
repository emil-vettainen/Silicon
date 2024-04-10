//document.addEventListener('DOMContentLoaded', function () {
//    function updateTheme(preferDark) {
//        const themeToggle = document.getElementById('theme-toggle');

//        if (preferDark) {
//            document.body.classList.add('dark-theme');

//            if (themeToggle) themeToggle.checked = true;
//        } else {
//            document.body.classList.remove('dark-theme');

//            if (themeToggle) themeToggle.checked = false;
//        }
//    }

//    const savedTheme = localStorage.getItem('theme');
//    const prefersDarkScheme = window.matchMedia('(prefers-color-scheme: dark)');

//    if (savedTheme) {
//        updateTheme(savedTheme === 'dark');
//    } else {
//        updateTheme(prefersDarkScheme.matches);
//    }

//    document.getElementById('theme-toggle').addEventListener('click', function () {
//        const isDarkMode = document.body.classList.contains('dark-theme');
//        const newTheme = isDarkMode ? 'light' : 'dark';
//        updateTheme(newTheme === 'dark');
//        localStorage.setItem('theme', newTheme);
//    });

//    prefersDarkScheme.addEventListener('change', event => {
//        if (!localStorage.getItem('theme')) {
//            updateTheme(event.matches);
//        }
//    });
//});