function updateTime(now) {
    const time = now.toLocaleTimeString('en-US', { 
        hour12: false, 
        hour: '2-digit', 
        minute: '2-digit' 
    });
    document.getElementById('current-time').textContent = time;
    
    const seconds = now.getSeconds();
    const progress = (seconds / 59) * 100;
    document.getElementById('progress-bar').style.width = `${progress}%`;
}

function scrollToCurrentHour(now) {
    const minutes = now.getMinutes();
    
    if (minutes === 0) {
        scrollToCurrentHourImmediate(now);
    }
}

function scrollToCurrentHourImmediate(now) {
    const currentHour = now.getHours();
    const targetHourElement = document.getElementById(`hour-${currentHour + 1}`);
    if (targetHourElement) {
        targetHourElement.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
}

function updateTimeIndicator(now) {
    const startAt = 0; // Match the F# startAt value
    const pixelsPerHour = 60;
    const hours = now.getHours() - startAt;
    const minutes = now.getMinutes();
    const top = (hours + minutes / 60) * pixelsPerHour;
    
    const indicator = document.getElementById('current-time-indicator');
    if (indicator) {
        indicator.style.top = `${top}px`;
    }
}

function processUpdates() {
    const now = new Date();
    updateTime(now);
    updateTimeIndicator(now);
    scrollToCurrentHour(now);
}

function handleVideo() {
    const video = document.querySelector("video");    
    video.addEventListener("play", function() {
        video.classList.add("running");
    });
    video.addEventListener("timeupdate", function() {                             
        video.currentTime > 6 ? video.currentTime = 4 : 0;
    });
    video.addEventListener("pause", function() {
        video.classList.remove("running");
    });
    let _ = video.play();
}

document.addEventListener("click", (event) => {
    if (event.target && event.target.id === "activate") {
        handleVideo();
        event.target.setAttribute("disabled", "disabled");
    }
});

setInterval(processUpdates, 1000);
const now = new Date();
updateTime(now);
scrollToCurrentHourImmediate(now); // Initial scroll on page load


setInterval(() => {
    console.log('Reloading');
    htmx.ajax('GET', '/api/photo', '#photo-frame')
    console.log('Realoading done');
}, 60000);



// Register Service Worker
if ('serviceWorker' in navigator) {
  window.addEventListener('load', () => {
    navigator.serviceWorker.register('/js/service-worker.js')
      .then(registration => {
        console.log('ServiceWorker registration successful');
      })
      .catch(err => {
        console.log('ServiceWorker registration failed: ', err);
      });
  });
}
