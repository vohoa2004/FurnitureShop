import React from 'react';
import { MDBPagination, MDBPaginationItem, MDBPaginationLink } from 'mdb-react-ui-kit';

export default function Pagination(props) {
    const { currentPage, onPageChange, totalPages } = props
    console.log(totalPages)
    return (
        <nav aria-label='...'>
            <MDBPagination size='lg' className='mb-0'>
                {[...Array(totalPages)].map((_, index) => (
                    <MDBPaginationItem
                        key={index + 1}
                        className={`page-item ${currentPage === index + 1 ? 'active' : ''}`}
                        aria-current={currentPage === index + 1 ? 'page' : undefined}
                    >
                        <MDBPaginationLink
                            tag='span'
                            className='page-link'
                            onClick={() => onPageChange(index + 1)}
                        >
                            {index + 1}
                            {currentPage === index + 1}
                        </MDBPaginationLink>
                    </MDBPaginationItem>
                ))}

            </MDBPagination>
        </nav>
    );
}