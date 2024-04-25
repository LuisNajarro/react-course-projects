import { useState } from 'react';

import Header from './components/Header.jsx';
import UserInput from './components/UserInput.jsx';
import Result from './components/Result.jsx';
import { calculateInvestmentResults } from './util/investment.js';

function App() {
  const [annualData, setAnnualData] = useState([]);

  function handleUserInputChange(userInput) {
    console.log('User Input: ', userInput);
    let data = calculateInvestmentResults(userInput);
    setAnnualData(data);
    console.log('Annual Data: ', data);
  }

  return (
    <>
      <Header />
      <UserInput onUserInputChange={handleUserInputChange} />
      <Result annualData={annualData} />
    </>
  );
}

export default App;
