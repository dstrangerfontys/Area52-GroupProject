// Dit script verzorgt de interactieve onderdelen van de startpagina:
//
// - Verbergt de booking-widget (het reserveringsbalkje onderin) zodra de
//   gebruiker naar de aanbiedingen-sectie scrollt, door de CSS-class
//   'hidden' toe te voegen.
//
// - Laat elementen (items, aanbiedingen, accommodaties) met een
//   fade-in/slide-up animatie in beeld verschijnen door de CSS-class
//   'visible' toe te voegen zodra ze in viewport komen.
//
// Dit script werkt alleen op de presentatie en raakt de domein- of
// datalaag niet; het is puur front-end gedrag.

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