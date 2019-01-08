export const onSort = (event, sortKey) => {
  const { customers } = this.state;
  const direction = this.state.sort.direction === 'asc' ? 'desc' : 'asc';
  const sortedData = customers.sort((a, b) => {
      if (sortKey === 'name') {
          const nameA = a.name.toLowerCase();
          const nameB = b.name.toLowerCase();
          if (nameA < nameB) return -1;
          if (nameA > nameB) return 1;
          return 0; 
      }
      // sorts by customer id
      return a.id - b.id;
  });

  if (direction === 'desc') sortedData.reverse();
  this.setState({ customers, sort: { direction } });
}