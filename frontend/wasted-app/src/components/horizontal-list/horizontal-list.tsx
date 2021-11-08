import React from "react"
import {ScrollView} from "react-native"
import {FlatList} from "react-native-gesture-handler"
import {View} from "react-native-ui-lib"
import {HorizontalListProps} from "./interfaces"

export const HorizontalList = ({items, renderItem}: HorizontalListProps) => {
  return (
    <ScrollView nestedScrollEnabled style={{}}>
      <View>
        <FlatList
          horizontal
          refreshing
          nestedScrollEnabled
          data={items}
          renderItem={renderItem}
          initialNumToRender={4}
          keyExtractor={item => item.id}
        />
      </View>
    </ScrollView>
  )
}
