import React from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';

const testimonials = [
    {
        quote: "Donec facilisis quam ut purus rutrum lobortis. Donec vitae odio quis nisl dapibus malesuada.",
        name: "Maria Jones",
        position: "CEO, Co-Founder, XYZ Inc.",
        image: "src/assets/images/person-1.png"
    },
    {
        quote: "Nullam ac aliquet velit. Aliquam vulputate velit imperdiet dolor tempor tristique.",
        name: "John Smith",
        position: "CTO, Co-Founder, ABC Ltd.",
        image: "src/assets/images/person-1.png"
    },
    {
        quote: "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
        name: "Jane Doe",
        position: "CFO, Example Corp.",
        image: "src/assets/images/person-1.png"
    }
];

const TestimonialSlider = () => {
    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        autoplay: true,
        autoplaySpeed: 2000,
    };

    return (
        <div style={{ maxWidth: '800px', margin: '0 auto', textAlign: 'center' }}>
            <h2 style={{ marginBottom: '30px' }}>Testimonials</h2>
            <Slider {...settings}>
                {testimonials.map((testimonial, index) => (
                    <div key={index}>
                        <blockquote style={{ fontStyle: 'italic', marginBottom: '20px' }}>
                            <p>&ldquo;{testimonial.quote}&rdquo;</p>
                        </blockquote>
                        <div>
                            <img src={testimonial.image} alt={testimonial.name} style={{ borderRadius: '50%', width: '80px', marginBottom: '10px', marginLeft: '45%' }} />
                            <h3>{testimonial.name}</h3>
                            <span style={{ fontStyle: 'italic', color: 'gray' }}>{testimonial.position}</span>
                        </div>
                    </div>
                ))}
            </Slider>
        </div>
    );
};

export default TestimonialSlider;
