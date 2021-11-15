import React, {useState} from "react"
import {Text} from "react-native"
import {Checkbox, DateTimePicker, Slider, Stepper, View} from "react-native-ui-lib"
import {TextFieldProps} from "react-native-ui-lib/generatedTypes/src/incubator"
import {DecreaseType} from "../../../../api/interfaces"
import {Props} from "./interfaces"

const textFieldCommonValues: TextFieldProps = {
  editable: false,
  centered: false
}

interface IPriceDecreasing {
  startDecreasingAt: string
  minimumPrice: number
  intervalTimeInMinutes: number
  amountPerInterval: number
  percentPerInterval: number
  decreaseType: DecreaseType
}

export const PriceDecreasing = ({food, setFood}: Props) => {
  const [minimumPrice, setMinimumPrice] = useState<Number>(food.currentPrice)
  const [startDecreasingAt, setStartDecreasingAt] = useState<Date>(new Date())
  const [decreaseType, setDecreaseType] = useState<DecreaseType>(food.decreaseType)
  const [decreaseStep, setDecreaseStep] = useState<Number>(0)

  const [priceDecreasing, setPriceDecreasing] = useState<IPriceDecreasing>({
    startDecreasingAt: food.startDecreasingAt,
    minimumPrice: food.minimumPrice,
    intervalTimeInMinutes: food.intervalTimeInMinutes,
    amountPerInterval: food.amountPerInterval,
    percentPerInterval: food.percentPerInterval,
    decreaseType: food.decreaseType
  })

  const onChangeStartDecreasingAt = (startDecreasingAt: Date) => {
    setStartDecreasingAt(startDecreasingAt)
    setPriceDecreasing({...priceDecreasing, startDecreasingAt: startDecreasingAt.toDateString()})
    setFood({...food, startDecreasingAt: startDecreasingAt.toDateString()})
  }

  const onChangeMinimumPrice = (minimumPrice: number) => {
    setMinimumPrice(minimumPrice)
    setPriceDecreasing({...priceDecreasing, minimumPrice})
    setFood({...food, minimumPrice})
  }

  const onChangeInterval = (intervalTimeInMinutes: number) => {
    setPriceDecreasing({...priceDecreasing, intervalTimeInMinutes})
    setFood({...food, intervalTimeInMinutes})
  }

  const onChangeAmountPerInterval = (amountPerInterval: number) => {
    setPriceDecreasing({...priceDecreasing, amountPerInterval})
    setFood({...food, amountPerInterval})
  }

  const onChangePercentPerInterval = (percentPerInterval: number) => {
    setPriceDecreasing({...priceDecreasing, percentPerInterval: percentPerInterval * 100})
    setFood({...food, percentPerInterval: percentPerInterval * 100})
  }

  const onChangeDecreaseType = (x: DecreaseType) => {
    setDecreaseType(x)
    setFood({...food, decreaseType: x})
  }

  return (
    <View
      flex
      style={{
        flexDirection: "column",
        alignContent: "center",
        justifyContent: "space-around",
        width: "100%",
        height: "100%"
      }}
    >
      <View>
        <DateTimePicker
          title="Start decreasing at"
          mode="time"
          display="default"
          value={startDecreasingAt}
          onChange={onChangeStartDecreasingAt}
        />
      </View>

      <View>
        <Text>Minimum price: {food.minimumPrice.toFixed(2)}</Text>
        <Slider
          minimumValue={0}
          maximumValue={food.currentPrice}
          value={food.minimumPrice}
          step={0.01}
          onValueChange={onChangeMinimumPrice}
        />
      </View>

      <View>
        <Text>Decrease price interval (mins)</Text>
        <Stepper
          small
          minValue={0}
          maxValue={180}
          step={5}
          value={food.intervalTimeInMinutes}
          onValueChange={onChangeInterval}
        />
      </View>

      <View>
        <View>
          <Checkbox
            value={decreaseType === DecreaseType.AMOUNT}
            label="Amount"
            onValueChange={() => {
              onChangeDecreaseType(DecreaseType.AMOUNT)
              setDecreaseStep(0)
            }}
          />
          <Checkbox
            style={{
              marginTop: "5%",
              marginBottom: "5%"
            }}
            value={decreaseType === DecreaseType.PERCENT}
            label="Percent"
            onValueChange={() => {
              onChangeDecreaseType(DecreaseType.PERCENT)
              setDecreaseStep(0)
            }}
          />
        </View>

        <View>
          <Text>
            Decrease step:{" "}
            {decreaseType === DecreaseType.AMOUNT
              ? food.amountPerInterval.toFixed(2) + "€"
              : food.percentPerInterval.toFixed(0) + "%"}
          </Text>
          <Slider
            minimumValue={0}
            maximumValue={decreaseType === DecreaseType.AMOUNT ? 10 : 1}
            value={0}
            step={0.01}
            onValueChange={x => {
              setDecreaseStep(x)
              decreaseType === DecreaseType.AMOUNT ? onChangeAmountPerInterval(x) : onChangePercentPerInterval(x)
            }}
          />
        </View>
      </View>
    </View>
  )
}
