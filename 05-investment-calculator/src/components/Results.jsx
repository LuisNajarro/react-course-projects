import { calculateInvestmentResults } from '../util/investment.js';
import { formatter } from '../util/investment';

export default function Results({ input }) {
  const annualData = calculateInvestmentResults(input);
  return (
    <table id="result" className="center">
      <thead>
        <tr>
          <th>Year</th>
          <th>Investment Value</th>
          <th>Interest (Year)</th>
          <th>Total Interest</th>
          <th>Invested Capital</th>
        </tr>
      </thead>
      <tbody>
        {annualData.map((data) => (
          <tr key={data.year}>
            <td>{data.year}</td>
            <td>{formatter.format(data.valueEndOfYear)}</td>
            <td>{formatter.format(data.interest)}</td>
            <td>{formatter.format(data.interest * data.year)}</td>
            <td>{formatter.format(data.annualInvestment * data.year)}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
