import { useEffect, useRef } from "react";

const ParticleBackground: React.FC = () => {
  const canvasRef = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;

    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    let animationFrameId: number;
    const particles: {
      x: number;
      y: number;
      color: string;
      alpha: number;
      vx: number;
      vy: number;
    }[] = [];
    const numParticles = 210;
    const colors = ["#7C3AED", "#9F7AEA", "#06B6D4", "#086877c5", "#2f0740"];
    const particleSpeed = 0.25;

    function initializeParticles(canvasElement: HTMLCanvasElement) {
      particles.length = 0;
      for (let i = 0; i < numParticles; i++) {
        particles.push({
          x: Math.random() * window.innerWidth,
          y: Math.random() * window.innerHeight,
          color: colors[Math.floor(Math.random() * colors.length)],
          alpha: Math.random() * 0.5 + 0.1,
          vx: (Math.random() - 0.5) * particleSpeed,
          vy: (Math.random() - 0.5) * particleSpeed,
        });
      }
    }

    function draw() {
      const canvasElement = canvasRef.current;
      if (!canvasElement) return;

      const context = canvasElement.getContext("2d");
      if (!context) return;

      canvasElement.width = window.innerWidth;
      canvasElement.height = window.innerHeight;
      context.clearRect(0, 0, canvasElement.width, canvasElement.height);

      particles.forEach((particle) => {
        context.beginPath();
        context.arc(particle.x, particle.y, 2, 0, Math.PI * 2);
        context.fillStyle = particle.color;
        context.globalAlpha = particle.alpha;
        context.fill();

        particle.x += particle.vx;
        particle.y += particle.vy;

        if (particle.x < 0 || particle.x > canvasElement.width) {
          particle.vx = -particle.vx;
        }
        if (particle.y < 0 || particle.y > canvasElement.height) {
          particle.vy = -particle.vy;
        }
      });

      animationFrameId = requestAnimationFrame(draw);
    }

    if (canvas) {
      initializeParticles(canvas);
    }
    draw();

    return () => {
      if (animationFrameId) {
        cancelAnimationFrame(animationFrameId);
      }
    };
  }, []);

  return (
    <canvas
      ref={canvasRef}
      className="fixed top-0 left-0 w-full h-full -z-10 bg-lava-lamp"
    />
  );
};

export default ParticleBackground;
