document.addEventListener("DOMContentLoaded", () => {
    // --- TAB BUTTONS (brand-btn) ---
    const buttons = document.querySelectorAll(".brand-btn");
    const sections = document.querySelectorAll(".brand-section");
    buttons.forEach(button => {
        button.addEventListener("click", () => {
            buttons.forEach(b => b.classList.remove("active"));
            sections.forEach(s => s.classList.remove("active"));
            button.classList.add("active");
            const targetId = button.getAttribute("data-target");
            const target = document.getElementById(targetId);
            if (target) target.classList.add("active");
        });
    });

    // --- DRAG TO SCROLL FOR slider-track ---
    const tracks = document.querySelectorAll(".slider-track");
    tracks.forEach(track => {
        let isDown = false;
        let startX;
        let scrollLeft;

        track.addEventListener('mousedown', (e) => {
            isDown = true;
            track.classList.add('dragging');
            startX = e.pageX - track.offsetLeft;
            scrollLeft = track.scrollLeft;
        });

        track.addEventListener('mouseleave', () => {
            isDown = false;
            track.classList.remove('dragging');
        });
        track.addEventListener('mouseup', () => {
            isDown = false;
            track.classList.remove('dragging');
        });
        track.addEventListener('mousemove', (e) => {
            if (!isDown) return;
            e.preventDefault();
            const x = e.pageX - track.offsetLeft;
            const walk = (x - startX) * 1.5; // tốc độ kéo
            track.scrollLeft = scrollLeft - walk;
        });

        // touch support (mobile)
        let touchStartX = 0, touchScrollLeft = 0;
        track.addEventListener('touchstart', (e) => {
            touchStartX = e.touches[0].pageX - track.offsetLeft;
            touchScrollLeft = track.scrollLeft;
        });
        track.addEventListener('touchmove', (e) => {
            const x = e.touches[0].pageX - track.offsetLeft;
            const walk = (x - touchStartX) * 1.5;
            track.scrollLeft = touchScrollLeft - walk;
        });
    });

    // --- ARROW BUTTONS: gắn tự động cho mọi .slider ---
    const sliders = document.querySelectorAll(".slider");
    sliders.forEach(slider => {
        const track = slider.querySelector(".slider-track");
        const leftBtn = slider.querySelector(".arrow.left");
        const rightBtn = slider.querySelector(".arrow.right");

        if (!track) return;

        // tính scroll amount: ưu tiên dựa vào 1 item width (nếu có), fallback = slider.clientWidth*0.9
        const firstImg = track.querySelector("img");
        const itemWidth = firstImg ? (firstImg.clientWidth + parseInt(getComputedStyle(firstImg).marginRight || 0)) : Math.floor(slider.clientWidth * 0.33);
        const imagesPerView = Math.max(1, Math.floor(slider.clientWidth / (itemWidth || 1)));
        const scrollAmount = itemWidth * imagesPerView || Math.floor(slider.clientWidth * 0.9);

        if (leftBtn) {
            leftBtn.addEventListener("click", () => {
                track.scrollBy({ left: -scrollAmount, behavior: "smooth" });
            });
        }
        if (rightBtn) {
            rightBtn.addEventListener("click", () => {
                track.scrollBy({ left: scrollAmount, behavior: "smooth" });
            });
        }
    });

});

// giữ hàm moveSlide nếu bạn vẫn dùng onclick inline ở HTML (tương thích)
window.moveSlide = function (sectionId, direction) {
    const slider = document.getElementById(sectionId);
    if (!slider) return;
    const track = slider.querySelector(".slider-track");
    if (!track) return;

    const firstImg = track.querySelector("img");
    let itemW = firstImg
        ? (firstImg.clientWidth + parseInt(getComputedStyle(firstImg).marginRight || 0))
        : Math.floor(slider.clientWidth * 0.33);

    const visibleItems = Math.max(1, Math.floor(slider.clientWidth / (itemW || 1)));
    const amount = itemW * visibleItems || Math.floor(slider.clientWidth * 0.9);

    track.scrollBy({ left: direction * amount, behavior: "smooth" });
};