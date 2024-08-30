import React, { useState, useEffect } from 'react'
import "./BlogDetails.css"
import Header from '../../components/Header/Header'
import Footer from '../../components/Footer/Footer'
import { getBlogById } from '../../apis/BlogAPI';
import { formattedDate } from '../../utils/format';

const BlogDetails = () => {

    const [blog, setBlog] = useState(null);

    const blogId = parseInt(window.location.pathname.split('/')[2])
    console.log(blogId)

    useEffect(() => {
        const fetchBlogDetails = async () => {
            try {
                const blogResponse = await getBlogById(blogId);
                console.log(blogResponse)
                setBlog(blogResponse.data);
            } catch (error) {
                console.error('Failed to fetch product details', error);
            }
        };
        fetchBlogDetails();
    }, [blogId]);

    if (blog == null) {
        return <div>Loading...</div>
    }

    return (
        <div>
            <Header></Header>

            {/* <!-- Page Content --> */}
            {/* <!-- Banner Starts Here --> */}
            <div className="heading-page header-text">
                <section className="page-heading">
                    <div className="container">
                        <div className="row">
                            <div className="col-lg-12">
                                <div className="text-content">
                                    <h4>Post Details</h4>
                                    <h2>{blog.title}</h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            {/* <!-- Banner Ends Here --> */}


            <section className="blog-posts grid-system">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-8">
                            <div className="all-blog-posts">
                                <div className="row">
                                    <div className="col-lg-12">
                                        <div className="blog-post">
                                            <div className="blog-thumb">
                                                <img src={blog.imageLink} alt="" />
                                            </div>
                                            <div className="down-content">
                                                <span>Lifestyle</span>
                                                {/* <a href="post-details.html"><h4>Aenean pulvinar gravida sem nec</h4></a> */}
                                                <ul className="post-info">
                                                    <li>Admin</li>
                                                    <li>{formattedDate(blog.createdAt)}</li>
                                                    {/* <li><a href="#">10 Comments</a></li> */}
                                                </ul>
                                                <div className='content' dangerouslySetInnerHTML={{ __html: blog.content }} />
                                                <div className="post-options">
                                                    <div className="row">
                                                        <div className="col-6">
                                                            <ul className="post-tags">
                                                                <li><i className="fa fa-tags"></i></li>
                                                                <li><a href="#">Best Templates</a>,</li>
                                                                <li><a href="#">TemplateMo</a></li>
                                                            </ul>
                                                        </div>
                                                        <div className="col-6">
                                                            <ul className="post-share">
                                                                <li><i className="fa fa-share-alt"></i></li>
                                                                <li><a href="#">Facebook</a>,</li>
                                                                <li><a href="#"> Twitter</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    {/* <div className="col-lg-12">
                                        <div className="sidebar-item comments">
                                            <div className="sidebar-heading">
                                                <h2>4 comments</h2>
                                            </div>
                                            <div className="content">
                                                <ul>
                                                    <li>
                                                        <div className="author-thumb">
                                                            <img src="assets/images/comment-author-01.jpg" alt="" />
                                                        </div>
                                                        <div className="right-content">
                                                            <h4>Charles Kate<span>May 16, 2020</span></h4>
                                                            <p>Fusce ornare mollis eros. Duis et diam vitae justo fringilla condimentum eu quis leo. Vestibulum id turpis porttitor sapien facilisis scelerisque. Curabitur a nisl eu lacus convallis eleifend posuere id tellus.</p>
                                                        </div>
                                                    </li>
                                                    <li className="replied">
                                                        <div className="author-thumb">
                                                            <img src="assets/images/comment-author-02.jpg" alt="" />
                                                        </div>
                                                        <div className="right-content">
                                                            <h4>Thirteen Man<span>May 20, 2020</span></h4>
                                                            <p>In porta urna sed venenatis sollicitudin. Praesent urna sem, pulvinar vel mattis eget.</p>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div className="author-thumb">
                                                            <img src="assets/images/comment-author-03.jpg" alt="" />
                                                        </div>
                                                        <div className="right-content">
                                                            <h4>Belisimo Mama<span>May 16, 2020</span></h4>
                                                            <p>Nullam nec pharetra nibh. Cras tortor nulla, faucibus id tincidunt in, ultrices eget ligula. Sed vitae suscipit ligula. Vestibulum id turpis volutpat, lobortis turpis ac, molestie nibh.</p>
                                                        </div>
                                                    </li>
                                                    <li className="replied">
                                                        <div className="author-thumb">
                                                            <img src="assets/images/comment-author-02.jpg" alt="" />
                                                        </div>
                                                        <div className="right-content">
                                                            <h4>Thirteen Man<span>May 22, 2020</span></h4>
                                                            <p>Mauris sit amet justo vulputate, cursus massa congue, vestibulum odio. Aenean elit nunc, gravida in erat sit amet, feugiat viverra leo.</p>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="col-lg-12">
                                        <div className="sidebar-item submit-comment">
                                            <div className="sidebar-heading">
                                                <h2>Your comment</h2>
                                            </div>
                                            <div className="content">
                                                <form id="comment" action="#" method="post">
                                                    <div className="row">
                                                        <div className="col-md-6 col-sm-12">
                                                            <fieldset>
                                                                <input name="name" type="text" id="name" placeholder="Your name" required="" />
                                                            </fieldset>
                                                        </div>
                                                        <div className="col-md-6 col-sm-12">
                                                            <fieldset>
                                                                <input name="email" type="text" id="email" placeholder="Your email" required="" />
                                                            </fieldset>
                                                        </div>
                                                        <div className="col-md-12 col-sm-12">
                                                            <fieldset>
                                                                <input name="subject" type="text" id="subject" placeholder="Subject" />
                                                            </fieldset>
                                                        </div>
                                                        <div className="col-lg-12">
                                                            <fieldset>
                                                                <textarea name="message" rows="6" id="message" placeholder="Type your comment" required=""></textarea>
                                                            </fieldset>
                                                        </div>
                                                        <div className="col-lg-12">
                                                            <fieldset>
                                                                <button type="submit" id="form-submit" className="main-button">Submit</button>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                    </div> */}
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="sidebar">
                                <div className="row">
                                    <div className="col-lg-12">
                                        <div className="sidebar-item search">
                                            <form id="search_form" name="gs" method="GET" action="#">
                                                <input type="text" name="q" className="searchText" placeholder="type to search..." autocomplete="on" />
                                            </form>
                                        </div>
                                    </div>
                                    <div className="col-lg-12">
                                        <div className="sidebar-item recent-posts">
                                            <div className="sidebar-heading">
                                                <h2>Recent Posts</h2>
                                            </div>
                                            <div className="content">
                                                <ul>
                                                    <li><a href="post-details.html">
                                                        <h5>Vestibulum id turpis porttitor sapien facilisis scelerisque</h5>
                                                        <span>May 31, 2020</span>
                                                    </a></li>
                                                    <li><a href="post-details.html">
                                                        <h5>Suspendisse et metus nec libero ultrices varius eget in risus</h5>
                                                        <span>May 28, 2020</span>
                                                    </a></li>
                                                    <li><a href="post-details.html">
                                                        <h5>Swag hella echo park leggings, shaman cornhole ethical coloring</h5>
                                                        <span>May 14, 2020</span>
                                                    </a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="col-lg-12">
                                        <div className="sidebar-item categories">
                                            <div className="sidebar-heading">
                                                <h2>Categories</h2>
                                            </div>
                                            <div className="content">
                                                <ul>
                                                    <li><a href="#">- Nature Lifestyle</a></li>
                                                    <li><a href="#">- Awesome Layouts</a></li>
                                                    <li><a href="#">- Creative Ideas</a></li>
                                                    <li><a href="#">- Responsive Templates</a></li>
                                                    <li><a href="#">- HTML5 / CSS3 Templates</a></li>
                                                    <li><a href="#">- Creative &amp; Unique</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="col-lg-12">
                                        <div className="sidebar-item tags">
                                            <div className="sidebar-heading">
                                                <h2>Tag Clouds</h2>
                                            </div>
                                            <div className="content">
                                                <ul>
                                                    <li><a href="#">Lifestyle</a></li>
                                                    <li><a href="#">Creative</a></li>
                                                    <li><a href="#">HTML5</a></li>
                                                    <li><a href="#">Inspiration</a></li>
                                                    <li><a href="#">Motivation</a></li>
                                                    <li><a href="#">PSD</a></li>
                                                    <li><a href="#">Responsive</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <Footer></Footer>
        </div>
    )
}

export default BlogDetails;