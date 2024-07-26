import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import BlogPage, { loader as postsLoader } from './pages/Blog.jsx';
import HomePage from './pages/Home.jsx';
import PostPage, { loader as postLoader } from './pages/Post.jsx';
import RootLayout from './pages/Root.jsx';

const router = createBrowserRouter([
  {
    path: '/',
    element: <RootLayout />,
    children: [
      {
        index: true,
        element: <HomePage />,
      },
      {
        path: 'posts',
        children: [
          { index: true, element: <BlogPage />, loader: postsLoader },
          { path: ':id', element: <PostPage />, loader: postLoader },
        ],
      },
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
