import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Navigation} from "react-native-navigation"
import {Button, Colors, Incubator, LoaderScreen, RadioButton, RadioGroup, Text, View} from "react-native-ui-lib"
import {getAllFood} from "../../../api/food"
import {FoodSortObject, FoodSortType, Food as IFood} from "../../../api/interfaces"
import {FoodsList} from "../../../components/foods-list"
import {setHomeRoot} from "../../../services/navigation"
import {HOME_BUTTON} from "../home-button"
import {FoodProps} from "./interfaces"

export const Food = ({componentId}: FoodProps) => {
  const [loading, setLoading] = useState(true)
  const [foods, setFoods] = useState([] as IFood[])
  const [renderedFoods, setRenderedFoods] = useState([] as IFood[])
  const [searchValue, setSearchValue] = useState("")
  const [sortVisible, setSortVisible] = useState(false)
  const [sortType, setSortType] = useState(FoodSortType.NAME)
  const [ascending, setAscending] = useState(true)
  const [pageNumber, setPageNumber] = useState(1)

  const fetchFoods = async ({sortType}: FoodSortObject = {sortType: FoodSortType.NAME}) => {
    setLoading(true)
    setFoods(await getAllFood({sortObject: {sortType: directionalSortType(sortType)}}))
    setLoading(false)
  }

  const directionalSortType = (sortType: FoodSortType) => {
    if (!ascending) {
      return (sortType + "_desc") as FoodSortType
    }
    return sortType
  }

  const onEndReached = async () => {
    const newFood = await getAllFood({
      sortObject: {sortType: directionalSortType(sortType)},
      pagination: {pageNumber: pageNumber + 1, pageSize: 10}
    })

    if (newFood.length) {
      setFoods(foods.concat(newFood))
      setPageNumber(pageNumber + 1)
    }
  }

  useEffect(() => {
    setRenderedFoods(foods)
  }, [foods])

  useEffect(() => {
    const filteredFoods = foods.filter(food => food.name.toLowerCase().includes(searchValue.toLowerCase()))
    setRenderedFoods(filteredFoods)
  }, [searchValue])

  useEffect(() => {
    fetchFoods({sortType})
  }, [ascending, sortType])

  useEffect(() => {
    fetchFoods()
    Navigation.mergeOptions(componentId, {topBar: {leftButtons: [HOME_BUTTON]}})
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
    <View flex>
      <View marginH-s8 marginT-s4 marginB-s3>
        <View row br20>
          <View flexG>
            <Incubator.TextField
              text70
              autoCapitalize="none"
              label="Search"
              value={searchValue}
              labelStyle={{marginBottom: 4}}
              placeholder="Name of food"
              fieldStyle={{borderBottomWidth: 1, borderColor: Colors.purple40, paddingBottom: 4}}
              onChangeText={setSearchValue}
            />
          </View>
          <Button
            bg-transparent
            marginR-s3
            iconStyle={{width: 21, height: 21}}
            iconSource={require("../../../../assets/control.png")}
            onPress={() => {
              setSortVisible(!sortVisible)
            }}
          />
          <Button
            bg-transparent
            marginR-s3
            iconStyle={{width: 21, height: 21}}
            iconSource={require("../../../../assets/sort.png")}
            onPress={() => {
              setAscending(!ascending)
            }}
          />
        </View>
      </View>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <FoodsList foods={renderedFoods} componentId={componentId} onEndReached={onEndReached} />
      )}
      <View bg-white br30 padding-s2 paddingH-s4 style={{...styles.filter, ...{opacity: sortVisible ? 100 : 0}}}>
        <Text marginB-s2>Sort by</Text>
        <RadioGroup
          collapsable
          initialValue={sortType}
          onValueChange={(type: FoodSortType) => {
            setSortType(type)
            setAscending(true)
          }}
        >
          <View marginV-s1>
            <RadioButton size={20} label="Name" value={FoodSortType.NAME} />
          </View>
          <View marginV-s1>
            <RadioButton size={20} label="Price" value={FoodSortType.PRICE} />
          </View>
          <View marginV-s1>
            <RadioButton size={20} label="Time" value={FoodSortType.TIME} />
          </View>
        </RadioGroup>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  filter: {
    position: "absolute",
    top: 60,
    right: 30,
    height: "auto",
    opacity: 100,
    borderColor: Colors.grey50,
    borderWidth: 1,
    shadowColor: Colors.black,
    shadowOpacity: 0.2,
    shadowOffset: {height: 0, width: 0}
  }
})
