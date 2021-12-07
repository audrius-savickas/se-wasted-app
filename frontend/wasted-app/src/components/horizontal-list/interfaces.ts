export interface HorizontalListProps {
  items: any
  renderItem: (item: any) => JSX.Element
  onEndReached: () => void
}
