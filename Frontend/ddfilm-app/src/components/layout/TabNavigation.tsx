interface TabNavigationProps {
  activeTab: "sessionMovies" | "participants" | "watchedMovies";
  onTabChange: (
    tab: "sessionMovies" | "participants" | "watchedMovies"
  ) => void;
}

const TabNavigation: React.FC<TabNavigationProps> = ({
  activeTab,
  onTabChange,
}) => (
  <div className="bg-[#0D0D0D]/80 rounded-3xl shadow-xl p-4 mb-6">
    <div className="flex justify-around">
      {["sessionMovies", "participants", "watchedMovies"].map((tab) => (
        <button
          key={tab}
          onClick={() => onTabChange(tab as any)}
          className={`w-full py-3 text-[#F2F2E9] ${
            activeTab === tab
              ? "bg-[#710973] font-bold text-white"
              : "bg-transparent"
          } rounded-lg hover:bg-[#2F0740] transition-all`}
        >
          {tab === "sessionMovies"
            ? "Session Movies"
            : tab === "participants"
            ? "Participants"
            : "Watched Movies"}
        </button>
      ))}
    </div>
  </div>
);

export default TabNavigation;
