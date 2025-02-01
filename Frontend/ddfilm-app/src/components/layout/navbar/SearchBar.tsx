interface SearchBarProps {
  onSearch: (query: string) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ onSearch }) => {
  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    onSearch(event.target.value);
  };

  return (
    <div className="relative">
      <input
        type="text"
        className="block p-2 pl-10 w-56 text-gray-900 bg-gray-100 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-pink-800"
        placeholder="Search movies..."
        onChange={handleInputChange}
      />
      <svg
        className="absolute left-3 top-2.5 w-4 h-4 text-gray-500"
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        strokeWidth={2}
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M11 19c4.418 0 8-3.582 8-8s-3.582-8-8-8-8 3.582-8 8 3.582 8 8 8zM19 19l-4-4"
        />
      </svg>
    </div>
  );
};

export default SearchBar;
