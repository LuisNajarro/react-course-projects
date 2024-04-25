import { useState } from 'react';

export default function UserInput({ onUserInputChange }) {
  const [initialInvestment, setInitialInvestment] = useState(0);
  const [annualInvestment, setAnnualInvestment] = useState(0);
  const [expectedReturn, setExpectedReturn] = useState(0);
  const [duration, setDuration] = useState(0);

  function handleChangeInitialInvestment(event) {
    onUserInputChange({
      initialInvestment: Number(event.target.value),
      annualInvestment: Number(annualInvestment),
      expectedReturn: Number(expectedReturn),
      duration: Number(duration),
    });
    setInitialInvestment(event.target.value);
    console.log('Initial Investment: ', event.target.value);
  }

  function handleChangeAnnualInvestment(event) {
    onUserInputChange({
      initialInvestment: Number(initialInvestment),
      annualInvestment: Number(event.target.value),
      expectedReturn: Number(expectedReturn),
      duration: Number(duration),
    });
    setAnnualInvestment(event.target.value);
    console.log('Annual Investment: ', event.target.value);
  }

  function handleChangeExpectedReturn(event) {
    onUserInputChange({
      initialInvestment: Number(initialInvestment),
      annualInvestment: Number(annualInvestment),
      expectedReturn: Number(event.target.value),
      duration: Number(duration),
    });
    setExpectedReturn(event.target.value);
    console.log('Expected Return: ', event.target.value);
  }

  function handleChangeDuration(event) {
    onUserInputChange({
      initialInvestment: Number(initialInvestment),
      annualInvestment: Number(annualInvestment),
      expectedReturn: Number(expectedReturn),
      duration: Number(duration),
    });
    setDuration(event.target.value);
    console.log('Duration: ', event.target.value);
  }

  return (
    <div id="user-input">
      <div className="input-group">
        <label htmlFor="initial-investment">Initial Investment</label>
        <input
          type="number"
          id="initial-investment"
          value={initialInvestment}
          onChange={handleChangeInitialInvestment}
        />
        <label htmlFor="annual-investment">Annual Investment</label>
        <input
          type="number"
          id="annual-investment"
          value={annualInvestment}
          onChange={handleChangeAnnualInvestment}
        />
      </div>
      <div className="input-group">
        <label htmlFor="expected-return">Expected Return</label>
        <input
          type="number"
          id="expected-return"
          value={expectedReturn}
          onChange={handleChangeExpectedReturn}
        />
        <label htmlFor="duration">Duration</label>
        <input
          type="number"
          id="duration"
          value={duration}
          onChange={handleChangeDuration}
        />
      </div>
    </div>
  );
}
