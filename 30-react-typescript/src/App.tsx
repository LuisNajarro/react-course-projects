import NewTodo from './components/NewTodo.tsx';
import Todos from './components/Todos.tsx';
import TodosContextProvider from './store/todo-context.tsx';

function App() {
  return (
    <TodosContextProvider>
      <NewTodo />
      <Todos />
    </TodosContextProvider>
  );
}

export default App;
