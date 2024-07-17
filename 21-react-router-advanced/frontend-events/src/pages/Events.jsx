import { useLoaderData, json } from 'react-router-dom';

import EventsList from '../components/EventsList';

function EventsPage() {
  const data = useLoaderData();

  // if (data.isError) {
  //   return <p>{data.message}</p>;
  // }
  const events = data.events;

  return <EventsList events={events} />;
}

export default EventsPage;

export async function loader() {
  const response = await fetch('https://localhost:7111/events');

  if (!response.ok) {
    // return { isError: true, message: 'Could not fetch events.' };
    // throw new Response(JSON.stringify({ message: 'Could not fetch events.' }), {
    //   status: 500,
    // });
    throw json({ message: 'Could not fetch events.' }, { status: 500 });
  } else {
    return response;
  }
}
