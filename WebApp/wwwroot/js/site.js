document.addEventListener('DOMContentLoaded', () => {
    const bookingWidget = document.querySelector('.booking-widget');
    const offersSection = document.querySelector('#aanbiedingen');

    if (!bookingWidget || !offersSection) {
        return;
    }

    const toggleBookingWidget = () => {
        const rect = offersSection.getBoundingClientRect();

        // Als de top van de aanbiedingen-sectie bovenin beeld komt of hoger:
        if (rect.top <= 80) {
            bookingWidget.classList.add('hidden');
        } else {
            bookingWidget.classList.remove('hidden');
        }
    };

    window.addEventListener('scroll', toggleBookingWidget);
    toggleBookingWidget();
});

document.addEventListener('DOMContentLoaded', () => {
    const bookingWidget = document.querySelector('.booking-widget');
    const offersSection = document.querySelector('#aanbiedingen');

    if (bookingWidget && offersSection) {
        const toggleBookingWidget = () => {
            const rect = offersSection.getBoundingClientRect();
            if (rect.top <= 80) {
                bookingWidget.classList.add('hidden');
            } else {
                bookingWidget.classList.remove('hidden');
            }
        };

        window.addEventListener('scroll', toggleBookingWidget);
        toggleBookingWidget();
    }

    // ---- animaties voor trust, offers, accommodaties ----
    const toAnimate = [
        ...document.querySelectorAll('.trust-item'),
        ...document.querySelectorAll('.offer-card'),
        ...document.querySelectorAll('.accommodation-card')
    ];

    if ('IntersectionObserver' in window) {
        const observer = new IntersectionObserver(
            entries => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('visible');
                        observer.unobserve(entry.target);
                    }
                });
            },
            {
                threshold: 0.2
            }
        );

        toAnimate.forEach(el => observer.observe(el));
    } else {
        // Fallback: als IntersectionObserver niet bestaat, alles meteen zichtbaar maken
        toAnimate.forEach(el => el.classList.add('visible'));
    }
});