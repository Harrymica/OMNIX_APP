function initializeParticles() {
    const particles = document.querySelectorAll('.particle');
    particles.forEach(particle => {
        const duration = Math.random() * 8 + 4;
        particle.style.animationDuration = `${duration}s`;
    });
}

