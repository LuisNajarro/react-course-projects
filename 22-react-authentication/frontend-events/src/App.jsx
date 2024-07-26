import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import EditEventPage from './pages/EditEvent.jsx';
import ErrorPage from './pages/Error.jsx';
import EventDetailPage, {
  loader as eventDetailLoader,
  action as deleteEventAction,
} from './pages/EventDetail.jsx';
import EventsPage, { loader as eventsLoader } from './pages/Events.jsx';
import EventsRootLayout from './pages/EventsRoot.jsx';
import HomePage from './pages/Home.jsx';
import NewEventPage from './pages/NewEvent.jsx';
import RootLayout from './pages/Root.jsx';
import { action as manipulateEventAction } from './components/EventForm.jsx';
import NewsletterPage, {
  action as newsletterAction,
} from './pages/Newsletter.jsx';
import AuthenticationPage, {
  action as authAction,
} from './pages/Authentication.jsx';
import { action as logoutAction } from './pages/Logout.js';
import { tokenLoader } from './util/auth.js';

const router = createBrowserRouter([
  {
    path: '/',
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    id: 'root',
    loader: tokenLoader,
    children: [
      { index: true, element: <HomePage /> },
      {
        path: 'events',
        element: <EventsRootLayout />,
        children: [
          {
            index: true,
            element: <EventsPage />,
            loader: eventsLoader,
          },
          {
            path: ':eventId',
            id: 'event-detail',
            loader: eventDetailLoader,
            children: [
              {
                index: true,
                element: <EventDetailPage />,
                action: deleteEventAction,
              },
              {
                path: 'edit',
                element: <EditEventPage />,
                action: manipulateEventAction,
              },
            ],
          },
          {
            path: 'new',
            element: <NewEventPage />,
            action: manipulateEventAction,
          },
        ],
      },
      {
        path: 'auth',
        element: <AuthenticationPage />,
        action: authAction,
      },
      {
        path: 'newsletter',
        element: <NewsletterPage />,
        action: newsletterAction,
      },
      {
        path: 'logout',
        action: logoutAction,
      },
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
