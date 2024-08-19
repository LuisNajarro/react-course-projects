import NewTodo from './components/NewTodo.tsx';
import Todos from './components/Todos.tsx';
import Todo from './models/todo.ts';

function App() {
  const todos = [new Todo('Learn React'), new Todo('Learn TypeScript')];

  const addTodoHandler = (text: string) => {};

  return (
    <div>
      <NewTodo onAddTodo={addTodoHandler} />
      <Todos items={todos} />
    </div>
  );
}

export default App;
