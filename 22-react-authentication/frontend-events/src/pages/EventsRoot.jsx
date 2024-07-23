import { Outlet } from 'react-router-dom';

import EventsNavigation from '../components/EventsNavigation.jsx';

function EventsRootLayout() {
  return (
    <>
      <EventsNavigation />
      <Outlet />
    </>
  );
}

export default EventsRootLayout;
