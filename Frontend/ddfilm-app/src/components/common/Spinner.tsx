const Spinner: React.FC = () => {
  const baseColors = ["#F244D5", "#710973", "#A62EFF", "#FF66E5"];

  const darkenColor = (hexColor: string, factor: number = 0.3): string => {
    let hex = hexColor.replace("#", "");
    if (hex.length === 3) {
      hex = hex[0] + hex[0] + hex[1] + hex[1] + hex[2] + hex[2];
    }
    const r = parseInt(hex.substring(0, 2), 16);
    const g = parseInt(hex.substring(2, 4), 16);
    const b = parseInt(hex.substring(4, 6), 16);

    const newR = Math.max(0, Math.floor(r * (1 - factor)));
    const newG = Math.max(0, Math.floor(g * (1 - factor)));
    const newB = Math.max(0, Math.floor(b * (1 - factor)));

    const toHex = (c: number) => {
      const hex = c.toString(16);
      return hex.length === 1 ? "0" + hex : hex;
    };

    return `#${toHex(newR)}${toHex(newG)}${toHex(newB)}`;
  };

  const darkColors = baseColors.map((color) => darkenColor(color));

  return (
    <div className="inline-flex relative w-20 h-20">
      <div
        className="absolute top-1.5 left-1.5 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-0"
        style={{
          backgroundColor: darkColors[0],
          boxShadow: `0 0 3px ${darkColors[0]}, 0 0 7px ${darkColors[0]}`,
        }}
      ></div>
      <div
        className="absolute top-1.5 left-8 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[.2s]"
        style={{
          backgroundColor: darkColors[1],
          boxShadow: `0 0 3px ${darkColors[1]}, 0 0 7px ${darkColors[1]}`,
        }}
      ></div>
      <div
        className="absolute top-1.5 right-1.5 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[.4s]"
        style={{
          backgroundColor: darkColors[2],
          boxShadow: `0 0 3px ${darkColors[2]}, 0 0 7px ${darkColors[2]}`,
        }}
      ></div>
      <div
        className="absolute top-8 left-1.5 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[.6s]"
        style={{
          backgroundColor: darkColors[3],
          boxShadow: `0 0 3px ${darkColors[3]}, 0 0 7px ${darkColors[3]}`,
        }}
      ></div>
      <div
        className="absolute top-8 left-8 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[.8s]"
        style={{
          backgroundColor: darkColors[0],
          boxShadow: `0 0 3px ${darkColors[0]}, 0 0 7px ${darkColors[0]}`,
        }}
      ></div>
      <div
        className="absolute top-8 right-1.5 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[1s]"
        style={{
          backgroundColor: darkColors[1],
          boxShadow: `0 0 3px ${darkColors[1]}, 0 0 7px ${darkColors[1]}`,
        }}
      ></div>
      <div
        className="absolute bottom-1.5 left-1.5 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[1.2s]"
        style={{
          backgroundColor: darkColors[2],
          boxShadow: `0 0 3px ${darkColors[2]}, 0 0 7px ${darkColors[2]}`,
        }}
      ></div>
      <div
        className="absolute bottom-1.5 left-8 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[1.4s]"
        style={{
          backgroundColor: darkColors[3],
          boxShadow: `0 0 3px ${darkColors[3]}, 0 0 7px ${darkColors[3]}`,
        }}
      ></div>
      <div
        className="absolute bottom-1.5 right-1.5 w-3.5 h-3.5 rounded-full animate-neon-pulse delay-[1.6s]"
        style={{
          backgroundColor: darkColors[0],
          boxShadow: `0 0 3px ${darkColors[0]}, 0 0 7px ${darkColors[0]}`,
        }}
      ></div>
    </div>
  );
};

export default Spinner;
