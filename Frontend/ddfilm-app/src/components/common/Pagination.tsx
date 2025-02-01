import React from "react";

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (pageNumber: number) => void;
}

const Pagination: React.FC<PaginationProps> = ({
  currentPage,
  totalPages,
  onPageChange,
}) => {
  const getVisiblePages = () => {
    const visiblePages: (number | string)[] = [];
    const delta = 2;

    if (currentPage > delta + 1) {
      visiblePages.push(1, "...");
    }

    for (
      let i = Math.max(1, currentPage - delta);
      i <= Math.min(totalPages, currentPage + delta);
      i++
    ) {
      visiblePages.push(i);
    }

    if (currentPage < totalPages - delta) {
      visiblePages.push("...", totalPages);
    }

    return visiblePages;
  };

  const visiblePages = getVisiblePages();

  return (
    <div className="flex justify-center mt-4">
      <button
        className={`px-4 py-2 mx-1 rounded-full ${
          currentPage === 1
            ? "bg-gray-500 text-gray-300 cursor-not-allowed"
            : "bg-[#0D0D0D] text-[#F244D5] hover:bg-[#710973] hover:text-white transition-all border border-[#F244D5]"
        }`}
        onClick={() => currentPage > 1 && onPageChange(currentPage - 1)}
        disabled={currentPage === 1}
      >
        &laquo;
      </button>

      {visiblePages.map((page, index) => (
        <button
          key={index}
          className={`px-4 py-2 mx-1 rounded-full ${
            page === currentPage
              ? "bg-[#F244D5] text-[#0D0D0D] font-semibold border border-[#710973]"
              : page === "..."
              ? "cursor-default bg-transparent text-gray-400"
              : "bg-[#0D0D0D] text-[#F244D5] hover:bg-[#710973] hover:text-white transition-all border border-[#F244D5]"
          }`}
          onClick={() => typeof page === "number" && onPageChange(page)}
          disabled={page === "..."}
        >
          {page}
        </button>
      ))}

      <button
        className={`px-4 py-2 mx-1 rounded-full ${
          currentPage === totalPages
            ? "bg-gray-500 text-gray-300 cursor-not-allowed"
            : "bg-[#0D0D0D] text-[#F244D5] hover:bg-[#710973] hover:text-white transition-all border border-[#F244D5]"
        }`}
        onClick={() =>
          currentPage < totalPages && onPageChange(currentPage + 1)
        }
        disabled={currentPage === totalPages}
      >
        &raquo;
      </button>
    </div>
  );
};

export default Pagination;
